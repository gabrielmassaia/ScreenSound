﻿using Microsoft.Data.SqlClient;
using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Database
{
    internal class ArtistaDAL
    {
        public IEnumerable<Artista> Listar()
        {
            var lista = new List<Artista>();
            using var connection = new Connection().ObterConexao();
            connection.Open();

            String sql = "SELECT * FROM Artistas";
            SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                string nomeArtista = Convert.ToString(dataReader["Nome"]);
                string bioArtista = Convert.ToString(dataReader["bio"]);
                int idArtista = Convert.ToInt32(dataReader["Id"]);

                Artista artista = new(nomeArtista, bioArtista) { Id = idArtista };
                lista.Add(artista);
            }
            return lista;
        }

        public void Adicionar(Artista artista)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "INSERT INTO Artistas (Nome, FotoPerfil, Bio) VALUES (@nome, @perfilPadrao, @bio)";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@nome", artista.Nome);
            command.Parameters.AddWithValue("perfilPadrao", artista.FotoPerfil);
            command.Parameters.AddWithValue("@bio", artista.Bio);

            int retorno = command.ExecuteNonQuery();

            Console.WriteLine($"Linhas afetadas: {retorno}");
        }

        public void Atualizar(Artista artista)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = ($"UPDATE Artistas SET Nome = @nome, Bio = @bio WHERE Id = @id");
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@nome", artista.Nome);
            command.Parameters.AddWithValue("perfilPadrao", artista.FotoPerfil);
            command.Parameters.AddWithValue("@bio", artista.Bio);

            int retorno = command.ExecuteNonQuery();

            Console.WriteLine($"Linhas afetadas: {retorno}");
        }

        public void Deletar(Artista artista)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = ($"DELETE FROM Artistas WHERE Id = @id");
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@id", artista.Id);

            int retorno = command.ExecuteNonQuery();

            Console.WriteLine($"Linhas afetadas: {retorno}");
        }
    }
}
