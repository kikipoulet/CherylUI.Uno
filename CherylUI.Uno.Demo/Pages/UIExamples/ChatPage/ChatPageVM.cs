using System.Collections.ObjectModel;


namespace CherylUI.Uno.Demo.Pages.UIExamples.ChatPage;

public class ChatMessage
{
    public string Message { get; set; }
    public bool Sent { get; set; }
}

public class ChatPageVM 
{
    
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
