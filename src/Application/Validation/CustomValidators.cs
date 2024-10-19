using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using FluentValidation;

namespace Application.Validation;

public static class CustomValidators
{
    public static IRuleBuilderOptions<T, string?> HaveMatchWithRegex<T>(this IRuleBuilder<T, string?> ruleBuilder,
        Regex regex)
    {
        return ruleBuilder.Must(s => s is null || regex.IsMatch(s) )
            .WithMessage("{PropertyName} does not match the regex.");
    }
    
    public static IRuleBuilderOptions<T, A> IsNotNullWhenNotNull<T, A, B>(this IRuleBuilder<T, A> ruleBuilder,
        Expression<Func<T, B>> property)
    {
        return ruleBuilder.Must((arg1, a, context) =>
            {
                context.MessageFormatter.AppendArgument("PropertyName2", GetPropertyInfo(arg1, property).Name);
                return property.Compile().Invoke(arg1) is null | a is not null;
            })
            .WithMessage("{PropertyName} is null while {PropertyName2} is not null.");
    }
    
    private static PropertyInfo GetPropertyInfo<TSource, TProperty>(
        TSource source,
        Expression<Func<TSource, TProperty>> propertyLambda)
    {
        if (propertyLambda.Body is not MemberExpression member)
            throw new ArgumentException($"Expression '{propertyLambda}' refers to a method, not a property.");

        if (member.Member is not PropertyInfo propInfo)
            throw new ArgumentException($"Expression '{propertyLambda}' refers to a field, not a property.");

        Type type = typeof(TSource);
        if (propInfo.ReflectedType != null && type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType))
            throw new ArgumentException(
                $"Expression '{propertyLambda}' refers to a property that is not from type {type}.");

        return propInfo;
    }
}