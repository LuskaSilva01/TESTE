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
        public User? Get(string id) => _usuarios.Find(u => u.Id == id && u.IsActive).FirstOrDefault();

        // Ler um usuário (ativo ou inativo) pelo ID
        public User? GetIncludeInactive(string id) =>
            _usuarios.Find(u => u.Id == id).FirstOrDefault();

        // Ler um usuário pelo Email
        public User? GetByEmail(string email) =>
            _usuarios.Find(u => u.Email == email && u.IsActive).FirstOrDefault();

        // Criar um novo usuário
        public User Create(User usuario)
        {
            usuario.PasswordHash = _passwordHasher.HashPassword(usuario, usuario.PasswordHash);

            _usuarios.InsertOne(usuario);
            return usuario;
        }
        // Update geral de todos os atributos
        public void Update(string id, User updatedUser)
        {
            var existingUser = _usuarios.Find(u => u.Id == id).FirstOrDefault();
            if (existingUser == null)
                throw new Exception("Usuário não encontrado.");

            if (!string.IsNullOrEmpty(updatedUser.PasswordHash) &&
                updatedUser.PasswordHash != existingUser.PasswordHash)
            {
                updatedUser.PasswordHash = _passwordHasher.HashPassword(updatedUser, updatedUser.PasswordHash);
            }
            else
            {
                updatedUser.PasswordHash = existingUser.PasswordHash;
            }

            updatedUser.Id = existingUser.Id;

            _usuarios.ReplaceOne(u => u.Id == id, updatedUser);
        }

        // Atualizar apenas o IsActive (exclusão lógica)
        public void SetActiveStatus(string id, bool isActive)
        {
            var update = Builders<User>.Update.Set(u => u.IsActive, isActive);
            _usuarios.UpdateOne(u => u.Id == id, update);
        }
    }
}
