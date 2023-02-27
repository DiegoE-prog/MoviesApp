using AutoMapper;
using Movies.API.Dtos.Category;
using Movies.API.Dtos.Movie;
using Movies.API.Dtos.Review;
using Movies.API.Dtos.User;
using Movies.API.Entities;

namespace Movies.API.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Category, GetCategoryDto>().ReverseMap();
            CreateMap<Category, CategoryToAddDto>().ReverseMap();
            CreateMap<Category, CategoryToUpdateDto>().ReverseMap();

            CreateMap<Movie, GetMovieDto>().ReverseMap();
            CreateMap<Movie, GetMovieWithoutCategoryDto>().ReverseMap();
            CreateMap<Movie, MovieToAddDto>().ReverseMap();
            CreateMap<Movie, MovieToUpdateDto>().ReverseMap();
            CreateMap<Movie, GetMovieForReviewDto>().ReverseMap();

            CreateMap<User, GetUserForReviewDto>().ReverseMap();

            CreateMap<Review, GetReviewDto>().ReverseMap();
            CreateMap<Review, GetReviewDtoWithoutNavigation>().ReverseMap();
            CreateMap<Review, ReviewToAddDto>().ReverseMap();
        }
    }
}
