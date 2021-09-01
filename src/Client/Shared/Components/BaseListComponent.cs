using BlazorSchoolManager.Client.Infrastructure.Managers;
using BlazorSchoolManager.Shared.Constants.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazorSchoolManager.Client.Shared.Components
{
    public class BaseListComponent : ComponentBase
    {
        ISnackbar _snackBar;
        IJSRuntime _jsRuntime;
        protected int _totalItems;
        protected int _currentPage;
        protected string _searchString = "";
        protected bool _dense = false;
        protected bool _striped = true;
        protected bool _bordered = false;

        protected ClaimsPrincipal _currentUser;
        protected bool _canCreate;
        protected bool _canEdit;
        protected bool _canDelete;
        protected bool _canExport;
        protected bool _canSearch;
        protected bool _loaded;

        protected async Task<DialogResult> GetDialogData(int itemId, IDialogService dialogService, string dialogMessage, string deleteContent)
        {
            var parameters = new DialogParameters
            {
                {nameof(Dialogs.DeleteConfirmation.ContentText), string.Format(deleteContent, itemId)}
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };

            return await dialogService.Show<Shared.Dialogs.DeleteConfirmation>(dialogMessage, parameters, options).Result;
        }

        protected async Task ExportToExcel(ISharedApiService serviceManager, string fileName, string snackBarMessage)
        {
            var response = await serviceManager.ExportToExcelAsync(_searchString);
            if (response.Succeeded)
            {
                await _jsRuntime.InvokeVoidAsync("Download", new
                {
                    ByteArray = response.Data,
                    FileName = $"{nameof(fileName).ToLower()}_{DateTime.Now:ddMMyyyyHHmmss}.xlsx",
                    MimeType = ApplicationConstants.MimeTypes.OpenXml
                });
                _snackBar.Add(snackBarMessage, Severity.Success);
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }
    }
}
