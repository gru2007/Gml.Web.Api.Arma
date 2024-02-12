using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Gml.Web.Api.Core.Messages;
using Gml.Web.Api.Core.Options;
using Gml.Web.Api.Core.Repositories;
using Gml.Web.Api.Core.Services;
using Gml.Web.Api.Dto.User;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Gml.Web.Api.Core.Handlers;

public class AuthHandler : IAuthHandler
{
    public static async Task<IResult> CreateUser(
        IUserRepository userRepository,
        IValidator<UserCreateDto> validator,
        IMapper mapper,
        UserCreateDto createDto)
    {
        var result = await validator.ValidateAsync(createDto);

        if (!result.IsValid)
        {
            return Results.BadRequest(ResponseMessage.Create(result.Errors, "Ошибка валидации",
                HttpStatusCode.BadRequest));
        }

        var user = await userRepository.CheckExistUser(createDto.Login, createDto.Email);

        if (user is not null)
            return Results.BadRequest(ResponseMessage.Create("Пользователь с указанными данными уже существует",
                HttpStatusCode.BadRequest));

        user = await userRepository.CreateUser(createDto.Email, createDto.Login, createDto.Password);

        return Results.Ok(ResponseMessage.Create(mapper.Map<UserAuthReadDto>(user), "Успешная регистрация",
            HttpStatusCode.OK));
    }

    public static async Task<IResult> AuthUser(
        IUserRepository userRepository,
        IValidator<UserAuthDto> validator,
        IMapper mapper,
        UserAuthDto authDto)
    {
        var result = await validator.ValidateAsync(authDto);

        if (!result.IsValid)
        {
            return Results.BadRequest(ResponseMessage.Create(result.Errors, "Ошибка валидации",
                HttpStatusCode.BadRequest));
        }

        var user = await userRepository.GetUser(authDto.Login, authDto.Password);

        if (user is null)
            return Results.BadRequest(ResponseMessage.Create("Неверный логин или пароль",
                HttpStatusCode.BadRequest));

        return Results.Ok(ResponseMessage.Create(mapper.Map<UserAuthReadDto>(user), "Успешная авторизация",
            HttpStatusCode.OK));
    }

    public static Task<IResult> UpdateUser(IUserRepository userRepository, UserUpdateDto userUpdateDto)
    {
        throw new NotImplementedException();
    }
}