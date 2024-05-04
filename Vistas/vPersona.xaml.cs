using pmejiaST5.Model;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace pmejiaST5.Vistas;

public partial class vPersona : ContentPage
{

    public vPersona()
	{
		InitializeComponent();
    }

    private void btnInsertar_Clicked(object sender, EventArgs e)
    {
        lblStatus.Text = ""; // Limpiar el texto del label de estado
        App.personRepo.AddNewPerson(txtName.Text);  // Agregar una nueva persona al repositorio utilizando el nombre ingresado en el Entry
        lblStatus.Text = App.personRepo.StatusMessage;  // Actualizar el texto del label con el mensaje de estado del repositorio

    }

    private void btnObtener_Clicked(object sender, EventArgs e)
    {
        lblStatus.Text = "";  // Limpiar el texto del label de estado
        List<Persona> people = App.personRepo.GetAllPeople();// Obtener todas las personas del repositorio
        ListaPersona.ItemsSource = people; // Establecer la lista de personas como origen de datos para la CollectionView
        lblStatus.Text = App.personRepo.StatusMessage;  // Actualizar el texto del label con el mensaje de estado del repositorio

    }

    private void btnEliminar_Clicked(object sender, EventArgs e)
    {
        {
            // Verifica si hay un elemento seleccionado en la CollectionView
            if (ListaPersona.SelectedItem != null)
            {   
                Persona selectedPerson = (Persona)ListaPersona.SelectedItem; // Obtiene la persona seleccionada de la CollectionView
                App.personRepo.DeletePerson(selectedPerson.Id); // Elimina la persona seleccionada utilizando el método DeletePerson del repositorio
                lblStatus.Text = App.personRepo.StatusMessage;  // Actualiza el texto del label con el mensaje de estado del repositorio

                // Actualizar la lista después de eliminar
                List<Persona> people = App.personRepo.GetAllPeople();
                ListaPersona.ItemsSource = people;
            }
            else
            {
                lblStatus.Text = "Seleccione una persona para eliminar.";  // mostrar un mensaje en el label si no hay ningún elemento seleccionado, 
            }
        }
    }


    private void btnActualizar_Clicked(object sender, EventArgs e)
    {
        // Verifica si hay un elemento seleccionado en la CollectionView y si se ha ingresado un nuevo nombre
        if (ListaPersona.SelectedItem != null && !string.IsNullOrEmpty(txtName.Text))
        {
            Persona selectedPerson = (Persona)ListaPersona.SelectedItem;  // Obtiene la persona seleccionada de la CollectionView
            int personId = selectedPerson.Id;
            string newName = txtName.Text; // La reeplaza por el nuevo nombre

            App.personRepo.UpdatePerson(personId, newName);  // Actualiza el nombre de la persona seleccionada utilizando el método UpdatePerson del repositorio
            lblStatus.Text = App.personRepo.StatusMessage;

            // Actualizar la lista después de actualizar
            List<Persona> people = App.personRepo.GetAllPeople();
            ListaPersona.ItemsSource = people;
        }
        else
        {
            //  mostrar un mensaje en el label si no hay ningún elemento seleccionado o no se ha ingresado un nuevo nombre
            lblStatus.Text = "Seleccione una persona y escriba un nuevo nombre para actualizar."; 
        }
    }
}

