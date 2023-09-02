using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using _1.UsersManagement.Domain.Models.Exceptions;
using _2.UsersManagement.Application.Interfaces.Languages;
using _2.UsersManagement.Application.Transients;
using _3.UsersManagement.Infrastructure__EFCore_.Persistence;
using GTranslate.Results;
using GTranslate.Translators;
using Microsoft.EntityFrameworkCore;

namespace _3.UsersManagement.Infrastructure__EFCore_.Services.Languages
{
    public class TranslationService : ITranslationService
    {
        private readonly IGenericUnit _gUnit;
        private readonly CNTContext _cnt;
        public TranslationService(IGenericUnit gUnit, CNTContext cnt)
        {
            _gUnit = gUnit;
            _cnt = cnt;
        }
        
        public async Task<string> TranslateText(string text, string language)
        {
           return await UsingMicrosoftTranslator(text, language);
        }
        
        public string GetIso(string globalization)
        {
            return globalization.ToLower() switch
            {
                "es_mx" => "es_mx",
                "es_us" => "es_us",
                _ => "en"
            };
        }

        public async Task<object> TranslateEntities(object entitiesOrEntity, string iso)
        {
            if (entitiesOrEntity is IEnumerable<object> entities)
            {
                foreach (var entity in entities)
                {
                    await TranslateEntity(entity, iso);
                }
                return entities;
            }
            await TranslateEntity(entitiesOrEntity, iso);
            return entitiesOrEntity;
        }

        private async Task TranslateEntity(object entity, string iso)
        {
            foreach (var property in entity.GetType().GetProperties())
            {
                if (property.Name is "id" or "createdBy" or "modiffiedBy") continue;
                if (property.PropertyType != typeof(string)) continue;
                var originalValue = property.GetValue(entity)?.ToString();
                if (originalValue == null) continue;
                var translatedValue = await TranslateText(originalValue, iso);
                property.SetValue(entity, translatedValue);
            }
        }
        
        //-------------------------------------------------PRIVATE---------------------------------------------------//
        private async Task<string> UsingMicrosoftTranslator(string text, string language)
        {
            MicrosoftTranslationResult result;
            try
            {
                var translator = new MicrosoftTranslator();
                result = await translator.TranslateAsync(text, language);
            }
            catch (Exception)
            {
                return await UsingGoogleTranslator(text, language);
            }
            return result.Translation;
        }
        
        private async Task<string> UsingGoogleTranslator(string text, string language)
        {
            GoogleTranslationResult result;
            try
            {
                var translator = new GoogleTranslator2();
                result = await translator.TranslateAsync(text, language);
            }
            catch (Exception)
            {
                return await UsingYandexTranslator(text, language);
            }
            return result.Translation;
        }
        
        private async Task<string> UsingYandexTranslator(string text, string language)
        {
            YandexTranslationResult result;
            var translator = new YandexTranslator();
            result = await translator.TranslateAsync(text, language);
            return result.Translation;
        }
    }
}
