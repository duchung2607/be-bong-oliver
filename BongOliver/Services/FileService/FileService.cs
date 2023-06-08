using BongOliver.DTOs.Response;
using Firebase.Auth;
using Firebase.Storage;

namespace BongOliver.Services.FileService
{
    public class FileService : IFileService
    {
        private static string ApiKey = "AIzaSyBMWMGGNn5shx1twBqdXX7-QLDOn7Nuszs";
        private static string Bucket = "filestorage-5358d.appspot.com";
        private static string AuthEmail = "hungklyhongkl@gmail.com";
        private static string AuthPassword = "duydom2607";

        public async Task<string> UploadFile(FileStream stream, string fileName)
        {
            var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);
            var cancellation = new CancellationTokenSource();

            var task = new FirebaseStorage(
                Bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true
                })
                .Child("images")
                .Child(fileName)
                .PutAsync(stream, cancellation.Token);

            try
            {
                string link = await task;
                return link;
            }catch(Exception ex)
            {
                string test = ex.Message;
                return "0";
            }
        }

        public async Task<string> UploadFile(MemoryStream stream, string fileName)
        {
            var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);
            var cancellation = new CancellationTokenSource();

            var task = new FirebaseStorage(
                Bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true
                })
                .Child("images")
                .Child(fileName)
                .PutAsync(stream, cancellation.Token);

            try
            {
                string link = await task;
                return link;
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                return "0";
            }
        }
    }
}
