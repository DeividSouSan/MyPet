namespace Pet
{
    public class StatusEmojiCalculator
    {
        public static string HappinessStatus(int happinessStatus)
        {
            string message = "";
            if (happinessStatus < 20) message = "Muito triste 😭";
            else if (happinessStatus < 40) message = "Triste 😟";
            else if (happinessStatus < 60) message = "Normal 😐";
            else if (happinessStatus < 80) message = "Feliz 😀";
            else message = "Muito Feliz 😄";

            return $"{message} ({happinessStatus})";
        }

        public static string FoodStatus(int foodStatus)
        {
            string message = "";
            if (foodStatus > 80) message = "Cheio 😄";
            else if (foodStatus > 50) message = "Normal 😐";
            else if (foodStatus > 20) message = "Com fome 😟";
            else message = "Faminto 😭";

            return $"{message} ({foodStatus})";
        }
    }
}
