namespace bemi;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnCalculateClicked(object sender, EventArgs e)
    {
        if (!double.TryParse(WeightEntry.Text, out double weight) || weight <= 0)
        {
            DisplayAlert("Invalid Input", "Please enter a valid weight in kg.", "OK");
            return;
        }

        if (!double.TryParse(HeightEntry.Text, out double heightCm) || heightCm <= 0)
        {
            DisplayAlert("Invalid Input", "Please enter a valid height in cm.", "OK");
            return;
        }

        double heightM = heightCm / 100.0;
        double bmi = weight / (heightM * heightM);

        string category;
        string advice;
        Color color;

        if (bmi < 18.5)
        {
            category = "Underweight";
            advice = "Consider speaking with a healthcare provider about a healthy diet plan.";
            color = Colors.SteelBlue;
        }
        else if (bmi < 25.0)
        {
            category = "Healthy Weight";
            advice = "Great job! Keep maintaining your healthy lifestyle.";
            color = Colors.SeaGreen;
        }
        else if (bmi < 30.0)
        {
            category = "Overweight";
            advice = "Regular exercise and a balanced diet can help reach a healthy weight.";
            color = Colors.Orange;
        }
        else
        {
            category = "Obese";
            advice = "It's recommended to consult a healthcare professional for guidance.";
            color = Colors.Crimson;
        }

        BmiValueLabel.Text = bmi.ToString("F1");
        BmiValueLabel.TextColor = color;
        BmiCategoryLabel.Text = category;
        BmiCategoryLabel.TextColor = color;
        BmiAdviceLabel.Text = advice;
        ResultFrame.IsVisible = true;
    }
}
