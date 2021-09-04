using BlazorSchoolManager.Application.Features.Venues.Queries;
using BlazorSchoolManager.Application.Features.Lessons.Commands;
using BlazorSchoolManager.Application.Requests;
using BlazorSchoolManager.Client.Extensions;
using BlazorSchoolManager.Shared.Constants.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Blazored.FluentValidation;
using BlazorSchoolManager.Client.Infrastructure.Managers.Venue;
using BlazorSchoolManager.Client.Infrastructure.Managers.Lesson;
using BlazorSchoolManager.Application.Features.Teachers.Queries;

namespace BlazorSchoolManager.Client.Pages.Lessons
{
    public partial class AddEditLessonModal
    {
        [Inject] private ILessonManager LessonManager { get; set; }
        [Inject] private IVenueManager VenueManager { get; set; }

        [Parameter] public AddEditLessonCommand AddEditLessonModel { get; set; } = new();
        [CascadingParameter] private HubConnection HubConnection { get; set; }
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private List<GetVenuesResponsePaged> _venues = new();
        private List<GetTeachersResponsePaged> _teachers = new();

        public void Cancel()
        {
            MudDialog.Cancel();
        }

        private async Task SaveAsync()
        {
            var response = await LessonManager.SaveAsync(AddEditLessonModel);
            if (response.Succeeded)
            {
                _snackBar.Add(response.Messages[0], Severity.Success);
                await HubConnection.SendAsync(ApplicationConstants.SignalR.SendUpdateDashboard);
                MudDialog.Close();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
            HubConnection = HubConnection.TryInitialize(_navigationManager);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }

        private async Task LoadDataAsync()
        {
            await LoadImageAsync();
            await LoadVenuesAsync();
        }

        private async Task LoadVenuesAsync()
        {
            var data = await VenueManager.GetAsync(new GetVenuesRequest { });
            if (data.Succeeded)
            {
                _venues = data.Data;
            }
        }

        /**private async Task LoadTeachersAsync()
        {
            var data = await VenueManager.GetAsync(new GetVenuesRequest { });
            if (data.Succeeded)
            {
                _venues = data.Data;
            }
        }**/

        private async Task LoadImageAsync()
        {
            var data = await LessonManager.GetImageAsync(AddEditLessonModel.Id);
            if (data.Succeeded)
            {
                var imageData = data.Data;
                if (!string.IsNullOrEmpty(imageData))
                {
                    AddEditLessonModel.ImageDataURL = imageData;
                }
            }
        }

        private void DeleteAsync()
        {
            AddEditLessonModel.ImageDataURL = null;
            AddEditLessonModel.UploadRequest = new UploadRequest();
        }

        private IBrowserFile _file;

        private async Task UploadFiles(InputFileChangeEventArgs e)
        {
            _file = e.File;
            if (_file != null)
            {
                var extension = Path.GetExtension(_file.Name);
                var format = "image/png";
                var imageFile = await e.File.RequestImageFileAsync(format, 400, 400);
                var buffer = new byte[imageFile.Size];
                await imageFile.OpenReadStream().ReadAsync(buffer);
                //AddEditLessonModel.ImageDataURL = $"data:{format};base64,{Convert.ToBase64String(buffer)}";
                AddEditLessonModel.UploadRequest = new UploadRequest { Data = buffer, UploadType = Application.Enums.UploadType.Lesson, Extension = extension };
            }
        }

        private async Task<IEnumerable<int>> SearchVenues(string value)
        {
            // In real life use an asynchronous function for fetching data from an api.
            await Task.Delay(5);

            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(value))
                return _venues.Select(x => x.Id);

            return _venues.Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                .Select(x => x.Id);
        }

        private async Task<IEnumerable<int>> SearchTeachers(string value)
        {
            // In real life use an asynchronous function for fetching data from an api.
            await Task.Delay(5);

            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(value))
                return _teachers.Select(x => x.Id);

            return _teachers.Where(x => x.FirstName.Contains(value, StringComparison.InvariantCultureIgnoreCase) ||
                x.LastName.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                .Select(x => x.Id);
        }
    }
}