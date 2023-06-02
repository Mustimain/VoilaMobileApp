using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using VoilaMobileApp.Src.Base;
using VoilaMobileApp.Src.Database.DbContext;
using VoilaMobileApp.Src.Models;

namespace VoilaMobileApp.Src.ViewModels.ProfileVM.HelpVM
{
    public class HelpPageViewModel : BaseViewModel, IPageLifecycleAware, INavigationAware
    {
        private readonly FirebaseClient client;

        public HelpPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            client = DbContext.StartFirebaseClient();

        }



        private ObservableCollection<string> _notificationTypeList = new ObservableCollection<string>();
        public ObservableCollection<string> NotificationTypeList
        {
            get
            {
                return _notificationTypeList;
            }
            set
            {
                _notificationTypeList = value; RaisePropertyChanged();
            }
        }

        public void OnAppearing()
        {
            _notificationTypeList.Add("Öneri");
            _notificationTypeList.Add("Şikayet");
        }

        private string _detail;
        public string Detail
        {
            get
            {
                return _detail;
            }
            set
            {
                _detail = value; RaisePropertyChanged();
            }
        }

        private string _selectionNotifyType;
        public string SelectionNotifyType
        {
            get
            {
                return _selectionNotifyType;
            }
            set
            {
                _selectionNotifyType = value; RaisePropertyChanged();
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (!string.IsNullOrEmpty(SelectionNotifyType) && !string.IsNullOrEmpty(Detail))
                    {
                        try
                        {
                            var complaint = new Complaint
                            {
                                Id = Guid.NewGuid().ToString(),
                                CreateDate = DateTime.Now,
                                Message = Detail,
                                MessageType = SelectionNotifyType
                            };
                            await client.Child("Complaints").PostAsync(JsonConvert.SerializeObject(complaint));
                            await Toast.Make("Geri bildiriminiz için teşekkür ederiz.").Show();

                        }
                        catch (Exception ex)
                        {
                            await Toast.Make("Sistemsel bir hata meydana gelddi tekrar deneyiniz.").Show();

                        }
                    }
                    else
                    {
                        await Toast.Make("Alanlar boş bırakılamaz").Show();
                    }
                });
            }
        }
    }
}

