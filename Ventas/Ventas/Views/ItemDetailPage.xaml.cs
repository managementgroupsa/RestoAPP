using System.ComponentModel;
using Ventas.ViewModels;
using Xamarin.Forms;

namespace Ventas.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}