using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CherylUI.Uno.Demo.Pages.UIExamples.ChatPage;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class ChatPage : Page
{
    public ChatPage()
    {
        this.InitializeComponent();
       
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        Task.Run(() =>
        {
            Thread.Sleep(1000);
            
            DispatcherQueue.TryEnqueue(() =>
            {
                MySV.UpdateLayout();
                MySV.ChangeView(null, MySV.ScrollableHeight, null, true);
            });
        });
    }

    

    private void SendMessage(object sender, RoutedEventArgs e)
    {
        Messages.Add(new ChatMessage()
        {
            Sent = true,
            Message = TB.Text
        });
        TB.Text = "";
            MySV.ChangeView(null, MySV.ScrollableHeight, null, false);

    }
    
    public ObservableCollection<ChatMessage> Messages { get; } = new ObservableCollection<ChatMessage>()
{
    new ChatMessage { Sent = true, Message = "Hey! How's the weather over there? ☀️" },
    new ChatMessage { Sent = false, Message = "Hey! It's actually really nice today—clear skies and lots of sun 😎" },
    new ChatMessage { Sent = true, Message = "Lucky you! It's been raining nonstop here since this morning 🌧️" },
    new ChatMessage { Sent = false, Message = "Oh no, that's rough. Rainy days can really drag your mood down 😕" },
    new ChatMessage { Sent = true, Message = "Exactly. I had plans to go for a walk but ended up canceling everything 😩" },
    new ChatMessage { Sent = false, Message = "Maybe it's a good excuse to stay in and watch a movie or two? 🍿" },
    new ChatMessage { Sent = true, Message = "Haha, yeah, I was thinking the same. Something cozy with hot tea ☕" },
    new ChatMessage { Sent = false, Message = "Perfect combo! Here it’s warm enough to sit outside with a book 📖" },
    new ChatMessage { Sent = true, Message = "That sounds amazing. I miss sunshine like that 🌤️" },
    new ChatMessage { Sent = false, Message = "Don’t worry, your turn will come. Weather changes so quickly these days 😊" },
    new ChatMessage { Sent = true, Message = "True. They said the sun might peek out tomorrow afternoon, fingers crossed! 🤞" },
    new ChatMessage { Sent = false, Message = "I hope so! Everything feels better with just a little sunlight ☀️" },
    new ChatMessage { Sent = true, Message = "Totally. Even just opening the windows makes a difference 🪟" },
    new ChatMessage { Sent = false, Message = "Absolutely. Fresh air is underrated 🌬️" },
    new ChatMessage { Sent = true, Message = "Do you get four seasons where you live?" },
    new ChatMessage { Sent = false, Message = "Kind of, but winter is usually very mild. No snow, just rain and wind 🌧️💨" },
    new ChatMessage { Sent = true, Message = "Same here. I miss real winters sometimes ❄️" },
    new ChatMessage { Sent = false, Message = "Yeah, snow makes everything feel magical—until it turns to slush! 😂" },
    new ChatMessage { Sent = true, Message = "Haha exactly! Beautiful but not very practical 😅" },
    new ChatMessage { Sent = false, Message = "Still, I’d take snow over endless gray skies any day 🌫️" },
    new ChatMessage { Sent = true, Message = "Fair point. Hopefully we both get some decent weather soon 🌈" },
    new ChatMessage { Sent = false, Message = "Yes! Let me know if the sun finally shows up tomorrow ☀️" },
    new ChatMessage { Sent = true, Message = "I will! Maybe I’ll even go for that walk I skipped today 🚶‍♂️" },
    new ChatMessage { Sent = false, Message = "Sounds like a plan! 😄" },
};
}


public class BoolToAlignmentConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        return (bool)value ? HorizontalAlignment.Right : HorizontalAlignment.Left;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}

public class BoolToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        return (bool)value ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
