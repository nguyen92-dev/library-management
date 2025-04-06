using LibraryManagement.Models;

namespace LibraryManagement;

class Program
{
    static List<User> users = [];
    static List<Book> books = [];
    
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        while (true)
        {
            try
            {
                Console.WriteLine("---------------------------------------");
                Console.WriteLine("1. Thêm Sách");
                Console.WriteLine("2. Thêm Người Dùng");
                Console.WriteLine("3. Hiển Thị Sách");
                Console.WriteLine("4. Hiển Thị Người Dùng");
                Console.WriteLine("5. Tìm Sách Theo Tác Giả");
                Console.WriteLine("6. Xoá Sách Theo Id");
                Console.WriteLine("7. Xoá Người Dùng Theo Id");
                Console.WriteLine("8. Xoá Tất Cả Sách");
                Console.WriteLine("0. Thoát");
                Console.Write("Chọn một tùy chọn: ");
                string choice = Console.ReadLine();
            
                switch (choice)
                {
                    case "1":
                        AddBook();
                        break;
                    case "2":
                        AddUser();
                        break;
                    case "3":
                        DisplayBooks();
                        break;
                    case "4":
                        DisplayUsers();
                        break;
                    case "5":
                        SearchBooksByAuthor();
                        break;
                    case "6":
                        DeleteBooksById();
                        break;
                    case "7":
                        DeleteUsersById();
                        break;
                    case "8":
                        DeleteAllBooks();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Tùy chọn không hợp lệ. Vui lòng thử lại.");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Đã xảy ra lỗi: " + e.Message);
            }
        }        
    }

    private static void DeleteAllBooks()
    {
        books.RemoveAll(b => true);
        Console.WriteLine("Đã xóa tất cả sách.");
    }

    private static void DeleteBooksById()
    {
        Console.WriteLine("Nhập mã sách cần xóa: ");
        var id = HandleIntInput("ID không hợp lệ.");
        var bookToDelete = books.FirstOrDefault(book => book.Id == id);
        if (bookToDelete != null)
        {
            books.Remove(bookToDelete);
            Console.WriteLine("Xoá sách thành công!");
        }
        else
        {
            Console.WriteLine("Không tìm thấy sách với ID này.");
        }
    }
    
    private static void DeleteUsersById()
    {
        Console.WriteLine("Nhập mã người dùng cần xóa: ");
        var id = HandleIntInput("ID không hợp lệ.");
        var userToDelete = users.FirstOrDefault(user => user.Id == id);
        if (userToDelete != null)
        {
            users.Remove(userToDelete);
            Console.WriteLine("Xoá người dùng thành công!");
        }
        else
        {
            Console.WriteLine("Không tìm thấy người dùng với ID này.");
        }
    }

    private static void SearchBooksByAuthor()
    {
        Console.WriteLine("Nhập tên tác giả: ");
        var author = HandleRequiredStringInput("Tên tác giả");
        var foundBooks = books.Where(book => book.Author.Equals(author, StringComparison.OrdinalIgnoreCase)).ToList();
        if (foundBooks.Count() == 0)
        {
            Console.WriteLine("Không tìm thấy sách nào của tác giả này.");
            return;
        }
        foundBooks.ForEach(
            book => Console.WriteLine($"Mã: {book.Id}, " +
                                      $"Tên: {book.Title}, " +
                                      $"Tác Giả: {book.Author}, " +
                                      $"Năm: {book.PublicationYear}"));
    }

    private static void DisplayUsers()
    {
        if (users.Count == 0)
        {
            Console.WriteLine("Không có người dùng nào trong hệ thống.");
            return;
        }
        users.ForEach(user => Console.WriteLine($"{user.Name}"));
    }

    private static void DisplayBooks()
    {
        if (books.Count == 0)
        {
            Console.WriteLine("Không có sách nào trong thư viện.");
            return;
        }
        books.ForEach(book =>  
            Console.WriteLine($"Mã: {book.Id}, " +
                              $"Tên: {book.Title}, " +
                              $"Tác Giả: {book.Author}, " +
                              $"Năm: {book.PublicationYear}"));
    }

    private static void AddUser()
    {
        Console.WriteLine("Nhập ID Người Dùng: ");
        var id = HandleUniqueIdInput(users);
        Console.WriteLine("Nhập Tên Người Dùng: ");
        var name = HandleRequiredStringInput("Tên người dùng");
        var user = new User
        {
            Id = id,
            Name = name
        };
        users.Add(user);
        Console.WriteLine("Thêm người dùng thành công!");
    }

    private static void AddBook()
    {
        Console.Write("Nhập Mã Sách: ");
        var id = HandleUniqueIdInput(books);
        Console.Write("Nhập Tên Sách: ");
        var title = HandleRequiredStringInput("Tên sách");
        Console.Write("Nhập Tác Giả: ");
        var author = HandleRequiredStringInput("Tác giả");
        Console.Write("Nhập Năm Xuất Bản: ");
        var year = HandleIntInput("Năm xuất bản không hợp lệ.");
        
        var book = new Book
        {
            Id = id,
            Title = title,
            Author = author,
            PublicationYear = year
        };
        books.Add(book);
        Console.WriteLine("Thêm sách thành công!");
    }
    
    private static int HandleIntInput(string message)
    {
        int result;
        var input = Console.ReadLine();
        while (!int.TryParse(input, out result))
        {
            Console.WriteLine($"{message} Vui lòng nhập lại: ");
            input = Console.ReadLine();
        }
        return result;
    }
    
    private static string HandleStringInput(string message)
    {
        var input = Console.ReadLine();
        while (string.IsNullOrEmpty(input))
        {
            Console.WriteLine(message);
            input = Console.ReadLine();
        }
        return input;
    }
    
    private static string HandleRequiredStringInput(string fieldName)
    {
        return HandleStringInput($"{fieldName} không được để trống. Vui lòng nhập lại: ");
    }

    private static int HandleUniqueIdInput<T>(List<T> list) where T : Base
    {
        var id = HandleIntInput("ID không hợp lệ.");
        while (list.Any(@base => @base.Id == id))
        {
            Console.WriteLine("ID đã tồn tại. Vui lòng nhập lại: ");
            id = HandleIntInput("ID không hợp lệ.");
        }
        return id;
    }
}