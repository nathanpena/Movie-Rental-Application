using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dto;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        public IHttpActionResult GetMovies()
        {
            return Ok(_context.Movies.ToList().Select(Mapper.Map<Movie, MovieDto>));
        }

        public IHttpActionResult GetMovie(int id)
        {
            var movie = GetMovieHelper(id);
            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MovieDto>(movie));


        }

        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                BadRequest();

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            _context.Movies.Add(movie);
            _context.SaveChanges();
            movieDto.Id = movie.Id;
            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var dbMovie = GetMovieHelper(id);
            if (dbMovie == null)
                return NotFound();

            Mapper.Map(movieDto, dbMovie);

            _context.SaveChanges();

            return Ok(Mapper.Map<Movie, MovieDto>(dbMovie));
        }

        [HttpDelete]
        public HttpStatusCode DeleteMovie(int id)
        {
            var movie = GetMovieHelper(id);
            _context.Movies.Remove(movie);
            _context.SaveChanges();

            return HttpStatusCode.OK;
        }

        private Movie GetMovieHelper(int id)
        {
            return _context.Movies.SingleOrDefault(c => c.Id == id);

        }
    }
}
