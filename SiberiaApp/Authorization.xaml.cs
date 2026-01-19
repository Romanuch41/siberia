using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using System.Text.RegularExpressions;
using Android.App.Usage;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using SiberiaApp.Classes;
using SiberiaApp;

namespace SiberiaApp;

public partial class Authorization : ContentPage
{
	ICommand auth;
	int code;
	public Authorization()
	{
		InitializeComponent();
		auth = new RelayCommand<string>(AuthUser);

    }

	public async void SendCodeTelegram()
	{
		if (!string.IsNullOrEmpty(UserLogin.Text) && Regex.IsMatch(UserLogin.Text, @"^\+\d{11}$") )
		{
			var rnd = new Random();
			code = rnd.Next((int)Math.Pow(10, 4 - 1), (int)Math.Pow(10, 4) - 1);


			TelegramResponse response = new TelegramResponse();
			TelegramRquest request = new TelegramRquest();
			TelegramService telegramservice = new TelegramService(request, response);
			var result = await telegramservice.SendVerificationCode(UserLogin.Text, code.ToString());
			if (result.success)
			{
				SicretCode.IsVisible = true;
                SicretCode.IsEnabled = true;
				InputPhone.Text = "Ok";
				InputPhone.CommandParameter = "CheckCode";
            }
        }
	}

	public void CheckCode()
	{
		if (code.ToString() == SicretCode.Text)
		{

        }
	}

    public void AuthUser(string key)
	{
		switch(key)
		{
			case "telegram":
				SendCodeTelegram();
				break;
			case "CheckCode":
				CheckCode();
				break;
        }
	}
}