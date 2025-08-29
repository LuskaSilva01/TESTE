using MongoDB.Driver;
using TESTE.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using TESTE.Data;
using System.Collections.Generic;
using TESTE.Data.Entities;

namespace TESTE.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _usuarios;
        private readonly PasswordHasher<User> _passwordHasher;

        public UserService(MongoDbService mongoDbService)
        {
            _usuarios = mongoDbService.Users;
            _passwordHasher = new PasswordHasher<User>();
        }

        // Ler todos os usuários ativos
        public List<User> Get() => _usuarios.Find(u => u.IsActive).ToList();

        // Ler um usuário pelo ID
        public User Get(string id) => _usuarios.Find(u => u.Id == id && u.IsActive).FirstOrDefault();

        // Criar um novo usuário
        public User Create(User usuario)
        {
            usuario.PasswordHash = _passwordHasher.HashPassword(usuario, usuario.PasswordHash);

            _usuarios.InsertOne(usuario);
            return usuario;
        }

        // Exclusão lógica do usuário
        public void Delete(string id)
        {
            var usuario = _usuarios.Find(u => u.Id == id).FirstOrDefault();
            if (usuario != null)
            {
                usuario.IsActive = false;
                usuario.DeletedAt = DateTime.UtcNow;
                _usuarios.ReplaceOne(u => u.Id == id, usuario);
            }
        }
    }
}
