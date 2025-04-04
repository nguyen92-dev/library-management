namespace LibraryManagement;

class Program
{
    static List<User> users = new List<User>();
    static List<Book> books = new List<Book>();
    
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

    private static void DeleteBooksById()
    {
        Console.WriteLine("Nhap ma sach can xoa: ");
        int id = HandleIntInput("ID khong hop le. Vui long nhap lai: ");
        books.RemoveAll(book => book.BookId == id);
    }
    
    private static void DeleteUsersById()
    {
        Console.WriteLine("Nhap ma nguoi dung can xoa: ");
        int id = HandleIntInput("ID khong hop le. Vui long nhap lai: ");
        users.RemoveAll(user => user.UserId == id);
    }

    private static void SearchBooksByAuthor()
    {
        Console.WriteLine("Nhap ten tac gia: ");
        var author = Console.ReadLine();
        var foundBooks = books.Where(book => book.Author == author).ToList();
        if (foundBooks.Count() == 0)
        {
            Console.WriteLine("Khong tim thay sach cua tac gia nay.");
            return;
        }
        foundBooks.ForEach(
            book => Console.WriteLine($"Mã: {book.BookId}, " +
                                      $"Tên: {book.Title}, " +
                                      $"Tác Giả: {book.Author}, " +
                                      $"Năm: {book.PublicationYear}"));
    }

    private static void DisplayUsers()
    {
        if (users.Count == 0)
        {
            Console.WriteLine("Khong co nguoi dung nao trong thu vien.");
            return;
        }
        users.ForEach(user => Console.WriteLine($"{user.Name}"));
    }

    private static void DisplayBooks()
    {
        if (books.Count == 0)
        {
            Console.WriteLine("Khong co sach nao trong thu vien.");
            return;
        }
        books.ForEach(book =>  
            Console.WriteLine($"Mã: {book.BookId}, " +
                              $"Tên: {book.Title}, " +
                              $"Tác Giả: {book.Author}, " +
                              $"Năm: {book.PublicationYear}"));
    }

    private static void AddUser()
    {
        Console.WriteLine("Nhập ID Người Dùng: ");
        int id = HandleIntInput("ID không hợp lệ. Vui lòng nhập lại: ");
        Console.WriteLine("Nhập Tên Người Dùng: ");
        string? name = Console.ReadLine();
        User user = new User
        {
            UserId = id,
            Name = name
        };
        users.Add(user);
        Console.WriteLine("Thêm người dùng thành công!");
    }

    private static void AddBook()
    {
        Console.Write("Nhập Mã Sách: ");
        int id = HandleIntInput("Mã sách không hợp lệ. Vui lòng nhập lại: ");
        Console.Write("Nhập Tên Sách: ");
        string? title = Console.ReadLine();
        Console.Write("Nhập Tác Giả: ");
        string? author = Console.ReadLine();
        Console.Write("Nhập Năm Xuất Bản: ");
        int year = HandleIntInput("Năm xuất bản không hợp lệ. Vui lòng nhập lại: ");
        
        Book book = new Book
        {
            BookId = id,
            Title = title,
            Author = author,
            PublicationYear = year
        };
        books.Add(book);
        Console.WriteLine("Thêm sách thành công!");
    }
    
    public static int HandleIntInput(string message)
    {
        int result;
        var input = Console.ReadLine();
        while (!int.TryParse(input, out result))
        {
            Console.WriteLine(message);
            input = Console.ReadLine();
        }
        return result;
    }
}