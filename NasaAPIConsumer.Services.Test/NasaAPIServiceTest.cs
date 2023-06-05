using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NasaAPIConsumer.Domain;
using NasaAPIConsumer.Utils;
using System;
using System.Net;

namespace NasaAPIConsumer.Services.Test
{
    public class NasaAPIServiceTests
    {
        Mock<HttpClient> _httpClientMock;
        Mock<INasaAPIService> _serviceMock;
        Mock<IHttpService> _httpServiceMock;
        Mock<IDefaultHttpClientFactory> _httpClientFactoryMock;
        Mock<IServiceProvider> _serviceProvider;
        Mock<NasaAPIConfigurationsLoader> _configLoader;
        NasaAPIConfigurations _configs;
        List<Asteroid> _asteroids;

        [SetUp]
        public void Setup()
        {
            this._httpClientMock = new Mock<HttpClient>();
            this._serviceMock = new Mock<INasaAPIService>();
            this._httpServiceMock = new Mock<IHttpService>();
            this._httpClientFactoryMock = new Mock<IDefaultHttpClientFactory>();
            this._serviceProvider = new Mock<IServiceProvider>();
            this._configLoader = new Mock<NasaAPIConfigurationsLoader>();

            this._configs = new NasaAPIConfigurationsLoader().LoadConfigurations();
            this._asteroids = new() 
            { 
                new Asteroid() { 
                    Name = "277570 (2005 YP180)",
                    Diameter = 0.58283097f,
                    Velocity = "120346.4110554184",
                    Date = "2023-06-06",
                    Planet = "Earth"
                },
                new Asteroid() {
                    Name = "152685 (1998 MZ)",
                    Diameter = 0.58283097f,
                    Velocity = "50613.4615541184",
                    Date =  "2023-06-07",
                    Planet = "Earth"
                },
                new Asteroid() {
                    Name = "(2000 KA)",
                    Diameter = 0.19658148f,
                    Velocity = "96940.9555542227",
                    Date = "2023-06-06",
                    Planet = "Earth"
                }
            };
        }

        [Test]

        [TestCase(1)]
        public void Given_A_Day_Number_Between_1_And_7_Then_Returns_A_List_Of_Asteroids(int days)
        {
            // Arrange
            NasaAPIConfigurations configs = new()
            {
                ApiKey = "",
                BaseUrl = ""
            };

            _serviceProvider.Setup(x => x.GetService(typeof(IConfiguration)))
                .Returns(new ConfigurationManager());
            var serviceScope = new Mock<IServiceScope>();
            serviceScope.Setup(x => x.ServiceProvider).Returns(_serviceProvider.Object);

            var serviceScopeFactory = new Mock<IServiceScopeFactory>();
            serviceScopeFactory
                .Setup(x => x.CreateScope())
                .Returns(serviceScope.Object);
            _serviceProvider
            .Setup(x => x.GetService(typeof(IServiceScopeFactory)))
            .Returns(serviceScopeFactory.Object);

            this._configLoader.Setup(o => o.LoadConfigurations()).Returns(configs);            
            this._httpClientMock.Setup(o => o.GetAsync(It.IsAny<string>()))
                .Returns(() => Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)));
            this._serviceMock.Setup(o => o.Get(It.IsAny<int>())).ReturnsAsync(this._asteroids);
            this._httpServiceMock.Setup(o => o.GetAsync(It.IsAny<string>()))
                .Returns(() => Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)));
            this._httpClientFactoryMock.Setup(o => o.CreateHttpClient()).Returns(this._httpClientMock.Object);
            NasaAPIService nasaAPIService = new NasaAPIService(this._httpServiceMock.Object, this._configLoader.Object, this._httpClientFactoryMock.Object);

            // Act
            var response = nasaAPIService.Get(1).Result;
            // Assert

            Assert.That(response.Count, Is.GreaterThan(0));
        }
    }
}