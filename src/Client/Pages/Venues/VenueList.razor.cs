using BlazorSchoolManager.Shared.Constants.Permission;
using BlazorSchoolManager.Application.Features.Venues.Queries;
using BlazorSchoolManager.Application.Features.Venues.Commands;
using BlazorSchoolManager.Shared.Constants.Application;
using BlazorSchoolManager.Client.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using BlazorSchoolManager.Client.Shared;

namespace BlazorSchoolManager.Client.Pages.Venues
{
    public partial class VenueList : PartialClassBase
    {
        [CascadingParameter] private HubConnection HubConnection { get; set; }

        private IEnumerable<GetVenuesResponsePaged> _pagedData;
        private MudTable<GetVenuesResponsePaged> _table;
        private string _searchString = "";
        private int _totalItems;
        private int _currentPage;
        private bool _loaded;

        protected async Task OnInitializedAsync()
        {
            base.InitializePermissionsAsync();
            _loaded = true;

            HubConnection = HubConnection.TryInitialize(_navigationManager);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }

        private async Task<TableData<GetVenuesResponsePaged>> ServerReload(TableState state)
        {
            if (!string.IsNullOrWhiteSpace(_searchString))
            {
                state.Page = 0;
            }
            await LoadData(state.Page, state.PageSize, state);
            return new TableData<GetVenuesResponsePaged> { TotalItems = _totalItems, Items = _pagedData };
        }

        private void OnSearch(string text)
        {
            _searchString = text;
            _table.ReloadServerData();
        }

        private async Task LoadData(int pageNumber, int pageSize, TableState state)
        {
            string[] orderings = base.SetOrdering(state);

            var request = new GetVenuesRequest { PageSize = pageSize, PageNumber = pageNumber + 1, SearchString = _searchString, Orderby = orderings };
            var response = await VenueManager.GetAsync(request);
            ProcessGetResponse(response);
        }

        private void ProcessGetResponse(BlazorSchoolManager.Shared.Wrapper.PaginatedResult<GetVenuesResponsePaged> response)
        {
            if (response.Succeeded)
            {
                _totalItems = response.TotalCount;
                _currentPage = response.CurrentPage;
                _pagedData = response.Data;
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        private async Task ExportToExcel()
        {
            var response = await VenueManager.ExportToExcelAsync(_searchString);
            if (response.Succeeded)
            {
                await _jsRuntime.InvokeVoidAsync("Download", new
                {
                    ByteArray = response.Data,
                    FileName = $"{nameof(Venues).ToLower()}_{DateTime.Now:ddMMyyyyHHmmss}.xlsx",
                    MimeType = ApplicationConstants.MimeTypes.OpenXml
                });
                _snackBar.Add(string.IsNullOrWhiteSpace(_searchString)
                    ? _localizer["Venues exported"]
                    : _localizer["Filtered Venues exported"], Severity.Success);
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        private async Task InvokeModal(int id = 0)
        {
            var parameters = new DialogParameters();
            if (id != 0)
            {
                var venue = _pagedData.FirstOrDefault(c => c.Id == id);
                if (venue != null)
                {
                    parameters.Add(nameof(AddEditVenueModal), new AddEditVenueCommand
                    {
                        Id = venue.Id,
                        Name = venue.Name,
                        Description = venue.Description,
                        Capacity = venue.Capacity,
                        IsOnline = venue.IsOnline
                    });
                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditVenueModal>(id == 0 ? _localizer["Create"] : _localizer["Edit"], parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                OnSearch("");
            }
        }

        private async Task Delete(int id)
        {
            DialogResult result = await base.GetDialogResultAsync(id, _localizer["Delete Content"], _localizer["Delete"]);
            if (!result.Cancelled)
            {
                var response = await VenueManager.DeleteAsync(id);
                OnSearch("");
                await base.ProcessDeleteResponse(response);
            }
        }
    }
}
