using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ventas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClasificacionPage : ContentPage
    {
        public ClasificacionPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return true;
        }

        private async void btnCategoria_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(CategoriaPage)}");
        }

        private async void btnGrupo_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(GrupoPage)}");
        }

        private async void btnClase_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(ClasePage)}");
        }

        private async void btnFamilia_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(FamiliaPage)}");
        }

        private async void btnCancelar_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//DefaultPage");
        }
    }
}