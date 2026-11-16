
using CadastroEventos.Models;
using System.Globalization;
using CadastroEventos.Views;
namespace CadastroEventos;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        BindingContext = new Evento();
    }

    private async void CadastrarButton_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NomeEntry.Text) ||
            string.IsNullOrWhiteSpace(LocalEntry.Text) ||
            !int.TryParse(ParticipantesEntry.Text, out int participantes) ||
            participantes <= 0 ||
            !decimal.TryParse(CustoEntry.Text,
                NumberStyles.Number,
                CultureInfo.GetCultureInfo("pt-BR"),
                out decimal custo) ||
            custo < 0)
        {
            await DisplayAlert("Atenção",
                "Preencha todos os campos corretamente.", "OK");
            return;
        }

        DateTime inicio = DataInicioPicker.Date ?? DateTime.Today;
        DateTime termino = DataTerminoPicker.Date ?? DateTime.Today;

        if (termino < inicio)
        {
            await DisplayAlert("Atenção",
                "A data de término não pode ser anterior à data de início.",
                "OK");
            return;
        }

        var evento = new Evento
        {
            Nome = NomeEntry.Text,
            DataInicio = inicio,
            DataTermino = termino,
            NumeroParticipantes = participantes,
            Local = LocalEntry.Text,
            CustoPorParticipante = custo
        };

        await Navigation.PushAsync(new ResumoPage(evento));
    }
}
