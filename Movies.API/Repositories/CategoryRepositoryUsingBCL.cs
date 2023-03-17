using Microsoft.Data.SqlClient;
using Movies.Common.Models.Dtos.Category;
using Movies.DataAccess.Entities;
using Movies.API.Repositories.Interfaces;
using System.Data;

namespace Movies.API.Repositories
{
    public class CategoryRepositoryUsingBCL : ICategoryRepository
    {
        private readonly SqlConnection _connection;
        private SqlCommand? _command;

        public CategoryRepositoryUsingBCL(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<bool> AddCategoryAsync(CategoryToAddDto categoryToAddDto)
        {
            int result = 0;

            using(_connection)
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.Parameters.AddWithValue(@"Name", categoryToAddDto.Name);
                _command.CommandText = "[dbo].[usp_CreateCategory]";

                _connection.Open();
                result = await _command.ExecuteNonQueryAsync();
                _connection.Close();

            }

            return result > 0;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            using (_connection)
            {
                int result = 0;

                using (_connection)
                {
                    _command = _connection.CreateCommand();
                    _command.CommandType = CommandType.StoredProcedure;
                    _command.Parameters.AddWithValue(@"Id", id);
                    _command.CommandText = "[dbo].[usp_DeleteCategory]";

                    _connection.Open();
                    result = await _command.ExecuteNonQueryAsync();
                    _connection.Close();

                }

                return result > 0;
            }
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            var categories = new List<Category>();

            using (_connection)
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[dbo].[usp_GetAllCategories]";

                _connection.Open();

                SqlDataReader reader = _command.ExecuteReader();

                while(await reader.ReadAsync())
                {
                    Category category = new Category();

                    category.CategoryId = Convert.ToInt32(reader["CategoryId"]);
                    category.Name = reader["Name"].ToString()!;
                    category.IsActive = Convert.ToBoolean(reader["IsActive"]);

                    categories.Add(category);
                }

                _connection.Close();
            };

            return categories;
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var category = new Category();

            using (_connection)
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.Parameters.AddWithValue(@"Id", id);
                _command.CommandText = "[dbo].[usp_GetCategoryById]";

                _connection.Open();

                SqlDataReader reader = _command.ExecuteReader();

                while (await reader.ReadAsync())
                {
                    category.CategoryId = Convert.ToInt32(reader["CategoryId"]);
                    category.Name = reader["Name"].ToString()!;
                    category.IsActive = Convert.ToBoolean(reader["IsActive"]);
                }

                _connection.Close();
            }

            return category;
        }

        public async Task<bool> UpdateCategoryAsync(CategoryToUpdateDto categoryToUpdateDto)
        {
            using( _connection)
            {
                int result = 0;

                using (_connection)
                {
                    _command = _connection.CreateCommand();
                    _command.CommandType = CommandType.StoredProcedure;
                    _command.Parameters.AddWithValue(@"Id", categoryToUpdateDto.CategoryId);
                    _command.Parameters.AddWithValue(@"Name", categoryToUpdateDto.Name);
                    _command.CommandText = "[dbo].[usp_UpdateCategory]";

                    _connection.Open();
                    result = await _command.ExecuteNonQueryAsync();
                    _connection.Close();

                }

                return result > 0;
            }
        }
    }
}
