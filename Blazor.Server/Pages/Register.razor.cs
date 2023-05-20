using Contracts;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace Blazor.Server.Pages;

public partial class Register
{
    UserForCreationDto model = new UserForCreationDto();
    bool success;

    private void OnValidSubmit(EditContext context)
    {
        success = true;
        StateHasChanged();
        
    }

}