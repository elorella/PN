using ParkingRight.Contracts;
using ParkingRight.Integration.Tests.Setup;
using ParkingRight.Libs.Models;
using ParkingRight_Pre;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ParkingRight.Integration.Tests.Scenarios
{
    [Collection("api")]
    public class MovieTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        readonly HttpClient _client;

        public MovieTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task AddParkingRightDataReturnsOkStatus()
        {
            const int userId = 1;

            var response = await AddParkingRightData(userId);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        
        [Fact]
        public async Task GetAllItemsFromDatabaseReturnsNotNullMoiveResponse()
        {
            const int userId = 2;

            await AddParkingRightData(userId);

            var response = await _client.GetAsync("movies");

            MovieResponse[] result;
            using (var content = response.Content.ReadAsStringAsync())
            {
                result = JsonConvert.DeserializeObject<MovieResponse[]>(await content);
            }

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetMovieReturnsExpectedMovieName()
        {
            const int userId = 3;
            const string movieName = "Test-GetMovieBack";

            await AddParkingRightData(userId, movieName);

            var response = await _client.GetAsync($"movies/{userId}/{movieName}");

            MovieResponse result;
            using (var content = response.Content.ReadAsStringAsync())
            {
                result = JsonConvert.DeserializeObject<MovieResponse>(await content);
            }

            Assert.Equal(movieName, result.MovieName);
        }

        [Fact]
        public async Task UpdateMovieReturnsUpdatedParkingRightValue()
        {
            const int userId = 4;
            const string movieName = "Test-UpdateMovie";
            const int ranking = 10;

            await AddParkingRightData(userId, movieName);

            var updateMovie = new MovieUpdateRequest
            {
                MovieName = movieName,
                Ranking = ranking
            };

            var json = JsonConvert.SerializeObject(updateMovie);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            await _client.PatchAsync($"movies/{userId}", stringContent);

            var response = await _client.GetAsync($"movies/{userId}/{movieName}");

            MovieResponse result;
            using (var content = response.Content.ReadAsStringAsync())
            {
                result = JsonConvert.DeserializeObject<MovieResponse>(await content);
            }

            Assert.Equal(ranking, result.Ranking);
        }

        [Fact]
        public async Task GetMoviesRankingReturnsAnOverallParkingRighting()
        {
            const int userId = 5;
            const string movieName = "Test-GetMovieOverallRanking";

            await AddParkingRightData(userId, movieName);

            var response = await _client.GetAsync($"movies/{movieName}/ranking");

            ParkingRightResponse result;
            using (var content = response.Content.ReadAsStringAsync())
            {
                result = JsonConvert.DeserializeObject<ParkingRightResponse>(await content);
            }

            Assert.NotNull(result);
        }

        private async Task<HttpResponseMessage> AddParkingRightData(int testUserId, string movieName = "test-MovieName")
        {
            var movieDbData = new MovieDb
            {
                UserId = testUserId,
                MovieName = movieName,
                Description = "test-Description",
                Actors = new List<string>
                {
                    "testUser1",
                    "testUser2"
                },
                RankedDateTime = "5/10/2018 6:17:17 PM",
                Ranking = 4
            };

            var json = JsonConvert.SerializeObject(movieDbData);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            return await _client.PostAsync($"movies/{testUserId}", stringContent);
        }
    }
}
