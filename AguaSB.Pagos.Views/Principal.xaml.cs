﻿using AguaSB.Views;

using System.Windows;
using System.Windows.Controls;

namespace AguaSB.Pagos.Views
{
    public partial class Principal : UserControl, IView
    {
        public Principal()
        {
            InitializeComponent();
        }

        public FrameworkElement View => this;

        public void Entrar(object parametro)
        {
            System.Console.WriteLine($"Entrando con parámetro: {parametro}");
        }
    }
}
