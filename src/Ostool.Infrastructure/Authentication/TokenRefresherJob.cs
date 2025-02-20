using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ostool.Application.Abstractions.Logging;
using Ostool.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

//namespace Ostool.Infrastructure.Authentication
//{
//    internal class TokenRefresherJob : BackgroundService
//    {
//        private readonly IServiceProvider _serviceProvider;
//        private readonly ITestableLogger<TokenRefresherJob> _logger;

//        public TokenRefresherJob(IServiceProvider serviceProvider, ITestableLogger<TokenRefresherJob> logger)
//        {
//            _serviceProvider = serviceProvider;
//            _logger = logger;
//        }
//        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
//        {
//            while (true)
//            {
//                try
//                {
//                    using var scope = _serviceProvider.CreateScope();
//                    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//                    var refreshTokenContext = scope.ServiceProvider.GetRequiredService<RefreshTokenContext>();

//                    var tokens = dbContext.Tokens.ToList();

//                    foreach (var token in tokens)
//                    {
//                        if (token.ExpiresIn < DateTime.UtcNow.AddHours(1))
//                        {
//                            _logger.LogInformation("Refreshing Token");
//                            var result = await refreshTokenContext.RefreshTokenAsync(token, stoppingToken);

//                            token.AccessToken = result.AccessToken!;
//                            token.ExpiresIn = DateTime.UtcNow.AddSeconds(int.Parse(result.ExpiresIn!));

//                            dbContext.Tokens.Update(token);
//                            await dbContext.SaveChangesAsync();

//                            _logger.LogInformation("Token Refreshed : {0}" , result.AccessToken);
//                        }
//                    }

//                }
//                catch 
//                {
//                    _logger.LogError("Error While Refreshing Token");
//                }
//                finally
//                {
//                    await Task.Delay(600000);
//                }

//            }
//        }
//    }


//    public class RefreshTokenContext
//    {
//        private readonly HttpClient _httpClient;

//        public RefreshTokenContext(IHttpClientFactory httpClientFactory)
//        {
//            _httpClient = httpClientFactory.CreateClient();
//        }

//        public async Task<OAuthTokenResponse> RefreshTokenAsync(Token token, CancellationToken ct = default)
//        {
//            var tokenRequestParameters = new Dictionary<string, string>
//            {
//                { "grant_type", "refresh_token" },
//                { "refresh_token", token.RefreshToken! },
//                { "client_id", "536185920438-ep2nq7cgald1avipu16tudr9n8dqlg7d.apps.googleusercontent.com" },
//                { "client_secret", "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" }
//            };

//            var requestContent = new FormUrlEncodedContent(tokenRequestParameters);
//            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://accounts.google.com/o/oauth2/token")
//            {
//                Content = requestContent
//            };
//            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//            requestMessage.Version = _httpClient.DefaultRequestVersion;

//            var response = await _httpClient.SendAsync(requestMessage, ct);
//            var body = await response.Content.ReadAsStringAsync(ct);

//            return OAuthTokenResponse.Success(JsonDocument.Parse(body));
//        }
//    }
//}