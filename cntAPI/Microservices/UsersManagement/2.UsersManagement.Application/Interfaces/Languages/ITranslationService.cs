using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using _1.UsersManagement.Domain.Models.Exceptions;

namespace _2.UsersManagement.Application.Interfaces.Languages
{
    public interface ITranslationService
    {
        Task<string> TranslateText(string text, string language);
        string GetIso(string globalization);
    }
}