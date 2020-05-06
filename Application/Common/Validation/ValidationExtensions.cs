using Domain.SharedKernel.ValueObjects;
using FluentValidation;
using FluentValidation.Resources;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Application.Common.Validation
{
    public static class ValidationExtensions
    {
        public static IRuleBuilderOptions<T, string> Url<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator((IPropertyValidator)new URLValidator());
        }

        public static IRuleBuilderOptions<T, LocalizedString> Required<T>(
            this IRuleBuilder<T, LocalizedString> ruleBuilder)
        {
            return ruleBuilder.SetValidator((IPropertyValidator)new LocalizedStringValidator());
        }
        public static IRuleBuilderOptions<T, dynamic> RequiredFile<T>(
            this IRuleBuilder<T, dynamic> ruleBuilder)
        {
            return ruleBuilder.SetValidator((IPropertyValidator)new FileValidator());
        }
    }

    public class LocalizedStringValidator : PropertyValidator, IPropertyValidator
    {

        public LocalizedStringValidator() : base((IStringSource)new LanguageStringSource(nameof(LocalizedStringValidator)))
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            return context.PropertyValue != null && !string.IsNullOrEmpty(((LocalizedString)context.PropertyValue).Arabic) && !string.IsNullOrEmpty(((LocalizedString)context.PropertyValue).English);
        }

    }


    public class URLValidator : PropertyValidator, IRegularExpressionValidator, IPropertyValidator
    {
        private readonly Regex _regex;

        private const string _expression =
            @"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$";

        public URLValidator()
            : base((IStringSource)new LanguageStringSource(nameof(URLValidator)))
        {
            this._regex = new Regex(_expression);
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            return context.PropertyValue == null || this._regex.IsMatch((string)context.PropertyValue);
        }

        public string Expression => _expression;
    }


    public class FileValidator : PropertyValidator, IPropertyValidator
    {

        public FileValidator() : base((IStringSource)new LanguageStringSource(nameof(LocalizedStringValidator)))
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            return true;
        }

    }
}
