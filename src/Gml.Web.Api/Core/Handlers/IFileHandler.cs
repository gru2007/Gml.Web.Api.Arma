using System.Collections.Frozen;
using FluentValidation;
using Gml.Web.Api.Dto.Files;
using GmlCore.Interfaces;

namespace Gml.Web.Api.Core.Handlers;

public interface IFileHandler
{
    static abstract Task GetFile(
        HttpContext context,
        IGmlManager manager,
        string fileHash);

    static abstract Task<IResult> AddFileWhiteList(
        IGmlManager manager,
        IValidator<FrozenSet<FileWhiteListDto>> validator,
        FrozenSet<FileWhiteListDto> fileDto);

    static abstract Task<IResult> RemoveFileWhiteList(
        IGmlManager manager,
        IValidator<FrozenSet<FileWhiteListDto>> validator,
        FrozenSet<FileWhiteListDto> fileDto);

    static abstract Task<IResult> AddFolderWhiteList(
        IGmlManager manager,
        IValidator<FrozenSet<FolderWhiteListDto>> validator,
        FrozenSet<FolderWhiteListDto> folderDto);

    static abstract Task<IResult> RemoveFolderWhiteList(
        IGmlManager manager,
        IValidator<FrozenSet<FolderWhiteListDto>> validator,
        FrozenSet<FolderWhiteListDto> folderDto);
}
