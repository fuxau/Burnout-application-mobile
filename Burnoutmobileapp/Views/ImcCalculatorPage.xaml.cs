namespace Burnoutmobileapp.Views;

public partial class ImcCalculatorPage : ContentPage
{
    private bool _isMale = true;

    public ImcCalculatorPage()
    {
        InitializeComponent();
        CalculateImc();
    }

    private async void OnBackTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//profile");
    }

    private void OnMaleTapped(object sender, TappedEventArgs e)
    {
        _isMale = true;
        MaleButton.BackgroundColor = Color.FromArgb("#005da1");
        MaleButton.Stroke = Color.FromArgb("#005da1");
        MaleLabel.TextColor = Colors.White;
        MaleLabel.FontAttributes = FontAttributes.Bold;

        FemaleButton.BackgroundColor = Colors.Transparent;
        FemaleButton.Stroke = Color.FromArgb("#1a3a6b");
        FemaleLabel.TextColor = Color.FromArgb("#9CA3AF");
        FemaleLabel.FontAttributes = FontAttributes.None;

        CalculateImc();
    }

    private void OnFemaleTapped(object sender, TappedEventArgs e)
    {
        _isMale = false;
        FemaleButton.BackgroundColor = Color.FromArgb("#005da1");
        FemaleButton.Stroke = Color.FromArgb("#005da1");
        FemaleLabel.TextColor = Colors.White;
        FemaleLabel.FontAttributes = FontAttributes.Bold;

        MaleButton.BackgroundColor = Colors.Transparent;
        MaleButton.Stroke = Color.FromArgb("#1a3a6b");
        MaleLabel.TextColor = Color.FromArgb("#9CA3AF");
        MaleLabel.FontAttributes = FontAttributes.None;

        CalculateImc();
    }

    private void OnHeightChanged(object sender, ValueChangedEventArgs e)
    {
        HeightValue.Text = ((int)e.NewValue).ToString();
        CalculateImc();
    }

    private void OnWeightChanged(object sender, ValueChangedEventArgs e)
    {
        WeightValue.Text = ((int)e.NewValue).ToString();
        CalculateImc();
    }

    private void OnAgeChanged(object sender, ValueChangedEventArgs e)
    {
        AgeValue.Text = ((int)e.NewValue).ToString();
        CalculateImc();
    }

    private void CalculateImc()
    {
        double height = HeightSlider.Value / 100.0;
        double weight = WeightSlider.Value;
        double imc = weight / (height * height);

        ImcValue.Text = imc.ToString("F1");

        // Update status and color
        if (imc < 18.5)
        {
            ImcStatus.Text = "Insuffisance pondérale";
            ImcStatus.TextColor = Color.FromArgb("#3B82F6");
            ImcIndicator.Margin = new Thickness(imc / 40 * 280, 0, 0, 0);
        }
        else if (imc < 25)
        {
            ImcStatus.Text = "Normal";
            ImcStatus.TextColor = Color.FromArgb("#22C55E");
            ImcIndicator.Margin = new Thickness(70 + (imc - 18.5) / 6.5 * 70, 0, 0, 0);
        }
        else if (imc < 30)
        {
            ImcStatus.Text = "Surpoids";
            ImcStatus.TextColor = Color.FromArgb("#F59E0B");
            ImcIndicator.Margin = new Thickness(140 + (imc - 25) / 5 * 70, 0, 0, 0);
        }
        else
        {
            ImcStatus.Text = "Obésité";
            ImcStatus.TextColor = Color.FromArgb("#EF4444");
            ImcIndicator.Margin = new Thickness(210 + Math.Min((imc - 30) / 10 * 70, 60), 0, 0, 0);
        }
    }
}
