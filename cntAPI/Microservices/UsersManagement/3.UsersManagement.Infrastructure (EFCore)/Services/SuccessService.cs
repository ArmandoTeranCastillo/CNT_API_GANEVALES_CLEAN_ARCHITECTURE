using _2.UsersManagement.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using _1.UsersManagement.Domain.Models.Exceptions;
using _2.UsersManagement.Application.DTOs;
using _2.UsersManagement.Application.Interfaces.Languages;
using _2.UsersManagement.Application.Transients;
using _3.UsersManagement.Infrastructure__EFCore_.Persistence;
using UsersManagement.Common.Errors;
using UsersManagement.Common.Utilities;

namespace _3.UsersManagement.Infrastructure__EFCore_.Services
{
    public class SuccessService : ISuccessService
    {
        private readonly CNTContext _cnt;
        private readonly ITranslationService _translation;
        private readonly IGenericUnit _gUnit;
        public SuccessService(CNTContext cnt, ITranslationService translation, IGenericUnit gUnit)
        {
            _cnt = cnt;
            _translation = translation;
            _gUnit = gUnit;
        }

        //--------------------------------------------------------PUBLIC-----------------------------------------------------
        public async Task<ResponseDto<TDto>> HandleSuccess<TDto>(TDto data, string code, string controller, string language, string createdBy)
        {
            var message = await RegisterAndGetSuccess(data, code, controller, language, createdBy);
            return new ResponseDto<TDto>
            {
                Response = language == "es_mx" ? "Exitosa" : "OK",
                Data = data,
                Message = message
            };
        }

        //-------------------------------------------------------PRIVATE------------------------------------------------------
        private async Task<MessageDto> RegisterAndGetSuccess<TDto>(TDto request, string code, string controller, string language, string createdBy)
        {
            var messageSuccess = await GetSuccess(code, language);
            try
            {
                await PrepareRegisterSuccessInput(request, messageSuccess, controller, createdBy);
                var errorCode = await _gUnit.ErrorCode.GetSimpleById(i => i.CodeError == Codes.SuccessLog);
                var translation =  await _gUnit.Language.GetEntityPropertyOrNull(errorCode.Id, "idControl", language);
                messageSuccess.Log = translation ?? errorCode.DescriptionError;
            }
            catch (Exception)
            {
                var errorCode = await _gUnit.ErrorCode.GetSimpleById(i => i.CodeError == Codes.FailedLog);
                var translation =  await _gUnit.Language.GetEntityPropertyOrNull(errorCode.Id, "idControl", language);
                messageSuccess.Log = translation ?? errorCode.DescriptionError;
            }
            return messageSuccess;
        }

        //-------------------------------------------------------PRIVATE------------------------------------------------------
        private async Task<MessageDto> GetSuccess(string code, string language)
        {
            try
            {
                var errorCode = await _gUnit.ErrorCode.GetSimpleById(i => i.CodeError == code);
                var translation =  await _gUnit.Language.GetEntityPropertyOrNull(errorCode.Id, "idControl", language);
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
                return new MessageDto { Code = code, Description = "" };
            }
        }

        private async Task PrepareRegisterSuccessInput<TDto>(TDto request, MessageDto success, string cname, string createdBy)
        {
            var param = ConvertDtoToParams(request);
            await RegisterLogSuccess(param, success, cname, createdBy);
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

        private async Task RegisterLogSuccess(string param, MessageDto success, string controller, string createdBy)
        {
            try
            {
                var log = new LogErrors
                {
                    Id = Guid.NewGuid().ToString().ToUpper(),
                    Origin = controller,
                    FuncionName = controller,
                    CodeError = success.Code,
                    MessageError = success.Description,
                    Parameters = param,
                    CreatedAt = DateTime.Now,
                    CreatedBy = createdBy
                };
                await InsertCodeSuccessAsync(log);
            }
            catch (Exception)
            {
                throw new Exception(Codes.FailedInsertErrorLog);
            }
        }

        private async Task InsertCodeSuccessAsync(LogErrors log)
        {
            _cnt.LogErrors.Add(log);
            await _cnt.SaveChangesAsync();
        }
    }
}
