using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using System.Text.RegularExpressions;
//using Android.App.Usage;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using SiberiaApp.Classes;
using System.Diagnostics;

namespace SiberiaApp;

public partial class Authorization : ContentPage
{
	private int code;
	public Authorization()
	{
		InitializeComponent();
        BindingContext = this;
    }

	public async void SendCodeTelegram()
	{
		if (!string.IsNullOrEmpty(UserLogin.Text) || Regex.IsMatch(UserLogin.Text, @"^\+\d{11}$"))
		{
			GoogleDriveService driveservice = new GoogleDriveService(await FileSystem.OpenAppPackageFileAsync("service.json"));
			GoogleSheetsService sheetservice = new GoogleSheetsService(await FileSystem.OpenAppPackageFileAsync("service.json"));
			TableManager tableservice = new TableManager(sheetservice, await FileSystem.OpenAppPackageFileAsync("tablecontent.json"));
			Debug.WriteLine("Созданы объекты для API");

			var table = await tableservice.ReadTableAsync("Users");
			Debug.WriteLine("Получены данне пользователей");

			string[] phones = table.Select(row => row.Count >= 4 ? row[3]?.ToString() ?? "not data": "not data").ToArray();
			string[] chats = table.Select(row => row.Count >= 5 ? row[4]?.ToString() ?? "not data": "not data").ToArray();

            Debug.WriteLine(phones.ToString());
			string match = UserLogin.Text.Remove(0, 2);
			match = "8" + match;

			if (Array.Exists(phones, x => x == match))
			{
				string chat_id = chats[Array.IndexOf(phones, match)];

				var rnd = new Random();
				code = rnd.Next(1000, 10000);

				TelegramService telegramservice = new TelegramService(new TelegramRquest(), new TelegramResponse());
				var result = await telegramservice.SendVerificationCode(chat_id, code.ToString());
				if (result.success)
				{
					BorderSicretCode.IsVisible = true;
					BorderSicretCode.IsEnabled = true;
					InputPhone.Text = "Ok";
					InputPhone.CommandParameter = "CheckCode";
				}
				else
					await DisplayAlert("Ошибка", "Не удалось отправить код", "Ок");
			}
			else
				await DisplayAlert("Ошибка", "Пользователь не найден\nОбратитесь к администратору", "Ок");
		}
		else
			await DisplayAlert("Внимание", "Номер телефона введен не корректно", "Ок");
	}

	public async Task OpenTelegramBotAsync()
	{
        const string botUsername = "wild_Siberia_bot";

        var uri = new Uri($"https://t.me/{botUsername}");

        await Launcher.OpenAsync(uri);
    }

	public async void CheckCode()
	{
		if (code.ToString() == SicretCode.Text)
		{
            (Application.Current.MainPage as AppShell)?.ShowMainFlyOut();
			await Shell.Current.GoToAsync("//mainPage");
        }
	}

    private void AuthUser(object sender, EventArgs e)
	{
		switch(InputPhone.CommandParameter)
		{
			case "telegram":
				Debug.WriteLine("Получена команда на отправку кода");
				SendCodeTelegram();
				break;
			case "CheckCode":
				Debug.WriteLine("Получена команда на проверку кода");
				CheckCode();
				break;
		}
	}

    private void UserLogin_Focused(object sender, FocusEventArgs e)
    {
        if (sender is Entry entry)
        {
            // Переместить курсор в конец текста
            entry.CursorPosition = entry.Text?.Length ?? 0;
            entry.SelectionLength = 0;
        }
    }
}