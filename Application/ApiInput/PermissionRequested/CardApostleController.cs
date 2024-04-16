using System.Net;
using Chameleon.Application.CompanySetting.Business.Dtos;
using Chameleon.Application.CompanySetting.Business.Mappers;
using Chameleon.Application.CompanySetting.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chameleon.Application.ApiInput.PermissionRequested;

public class CardApostleController(IHttpContextAccessor cc, Context context) : BaseController(cc, context)
{
}