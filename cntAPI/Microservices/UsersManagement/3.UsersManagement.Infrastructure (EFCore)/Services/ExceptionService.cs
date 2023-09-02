using System;
using System.Linq;
using System.Threading.Tasks;
using _1.UsersManagement.Domain.Models.Exceptions;
using _2.UsersManagement.Application.DTOs;
using _2.UsersManagement.Application.DTOs.Exceptions;
using _2.UsersManagement.Application.Interfaces;
using _2.UsersManagement.Application.Transients;
using _3.UsersManagement.Infrastructure__EFCore_.Persistence;
using UsersManagement.Common.Errors;
using UsersManagement.Common.Utilities;

namespace _3.UsersManagement.Infrastructure__EFCore_.Services
{
    public class ExceptionService : IExceptionService
    {

        private readonly CNTContext _cnt;
        private readonly IGenericUnit _gUnit;
        public ExceptionService(CNTContext  cnt, IGenericUnit gUnit)
        {
            _cnt = cnt; 
            _gUnit = gUnit;
        }

        //---------------------------------------------------PUBLIC---------------------------------------------------------------------------------
        public async Task<ExceptionDto> HandleFailure<TDto>(TDto data, Exception ex, string controller, string language, string createdBy)
        {
            var responseError = await SearchException(data, ex, controller, language, createdBy);
            return ex switch
            {
                NotFoundException => new ExceptionDto { Status = 404, Error = responseError },
                BadRequestException => new ExceptionDto { Status = 400, Error = responseError },
                _ => new ExceptionDto { Status = 500, Error = responseError }
            };
        }

        private async Task<ResponseDto<TDto>> SearchException<TDto>(TDto data, Exception ex, string controller, string language, string createdBy)
        {
            var message = await RegisterAndGetError(data, ex, controller, language, createdBy);
            return new ResponseDto<TDto>
            {
                Response = language == "es_mx" ? "Fallida" : "Bad",
                Message = message
            };
        }

        private async Task<MessageDto> RegisterAndGetError<TDto>(TDto request, Exception ex, string controller, string language, string createdBy)
        {
            var error = await GetError(ex.Message, language);
            try
            {
                await PrepareRegisterErrorInput(request, ex, error.Description, controller, createdBy);
                var errorCode = await _gUnit.ErrorCode.GetSimpleById(i => i.CodeError == Codes.SuccessLog);
                var translation =  await _gUnit.Language.GetEntityPropertyOrNull(errorCode.Id, "idControl", language);
                error.Log = translation ?? errorCode.DescriptionError;
            }
            catch (Exception)
            {
                var errorCode = await _gUnit.ErrorCode.GetSimpleById(i => i.CodeError == Codes.FailedLog);
                var translation =  await _gUnit.Language.GetEntityPropertyOrNull(errorCode.Id, "idControl", language);
                error.Log = translation ?? errorCode.DescriptionError;
            }
            return error;
        }
        
        //--------------------------------------------------PRIVATE---------------------------------------------------------------------------------

        private async Task<MessageDto> GetError(string code, string language)
        {
            try {
                var errorCode = await _gUnit.ErrorCode.GetSimpleById(i => i.CodeError == code);
                var translation =  await _gUnit.Language.GetEntityPropertyOrNull(errorCode.IdDescriptionError, "idControl", language);
                var translationHelp =  await _gUnit.Language.GetEntityPropertyOrNull(errorCode.IdHelpError, "id", language);
                if (errorCode is null) throw new Exception();
                return new MessageDto
                {
                    Code = code,
                    Help = translationHelp ?? errorCode.Help,
                    Description = translation ?? errorCode.DescriptionError
                };
            }
            catch (Exception)
            {
                return new MessageDto { Code = "unidentified", Description = code };
            }      
        }

        private async Task PrepareRegisterErrorInput<TDto>(TDto request, Exception ex, string errorDesc, string cNamesConstant, string createdBy)
        {
            var param = ConvertDtoToParams(request);
            await RegisterLogError(ex, cNamesConstant, param, createdBy, errorDesc);
        }

        private static string ConvertDtoToParams<TDto>(TDto request)
        {
            try
            {
                return System.Text.Json.JsonSerializer.Serialize(request);
            }
            catch (InvalidOperationException)
            {
                throw new InternalServerError(Codes.EmptyField);
            }
        }

        private async Task RegisterLogError(Exception ex, string controller, string param, string isUser, string description)
        {
            try
            {
                var stackFrames = ex.StackTrace?.Split(" in ", StringSplitOptions.RemoveEmptyEntries);
                var truncatedString = stackFrames?.FirstOrDefault();

                var logError = new LogErrors
                {
                    Id = Guid.NewGuid().ToString().ToUpper(),
                    Origin = $"{ex.Source}{controller}",
                    FuncionName = truncatedString,
                    CodeError = ex.Message,
                    MessageError = description,
                    Parameters = param,
                    CreatedAt = DateTime.Now,
                    CreatedBy = isUser
                };

                await InsertCodeErrorAsync(logError);
            }
            catch (Exception)
            {
                throw new Exception(Codes.FailedInsertErrorLog);
            }
        }

        private async Task InsertCodeErrorAsync(LogErrors errors)
        {
            _cnt.LogErrors.Add(errors);
            await _cnt.SaveChangesAsync();
        }
    }
}
