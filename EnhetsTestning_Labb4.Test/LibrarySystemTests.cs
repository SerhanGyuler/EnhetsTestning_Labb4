namespace EnhetsTestning_Labb4.Test
{
    [TestClass]
    public sealed class LibrarbySystemTests
    {
        //Bokhantering

        // Test Case 1
        [TestMethod]
        public void AddBook_WithUniqueISBN_ShouldReturnTrue()
        {
            // Arrange
            var library = new LibrarySystem();
            var book = new Book("Test Title", "Test Author", "1234567890", 2020);

            // Act
            bool result = library.AddBook(book);

            // Assert
            Assert.IsTrue(result, "AddBook should return true when adding a book with a unique ISBN.");
        }

        // Test Case 2
        [TestMethod]
        public void AddBook_WithDuplicateISBN_ShouldReturnFalse()
        {
            // Arrange
            var library = new LibrarySystem();
            var book1 = new Book("First Book", "Author A", "9780451524935", 2019);
            library.AddBook(book1); // Add the first book

            var book2 = new Book("Second Book", "Author B", "9780451524935", 2020); // Duplicate ISBN

            // Act
            bool result = library.AddBook(book2);  // Try adding a second book with the same ISBN

            // Assert
            Assert.IsFalse(result, "AddBook should return false when attempting to add a book with a duplicate ISBN.");
        }

        // Test Case 3
        [TestMethod]
        public void AddBook_WithNoISBN_ShouldReturnFalse()
        {
            // Arrange
            var library = new LibrarySystem();
            var book1 = new Book("First Book", "Author A", "", 2019);

            // Act
            bool result = library.AddBook(book1);

            // Assert
            Assert.IsFalse(result, "AddBook should return false when trying to add a book with a no ISBN.");

        }

        // Test Case 4
        [TestMethod]
        public void RemoveBook_InstockAndNotLoanedOut_ShouldReturnTrue()
        {
            // Arrange
            var library = new LibrarySystem();
            var book = library.SearchByISBN("9780451524935");

            // Check if the book is borrowed or not
            book.IsBorrowed = false;

            // Act
            bool result = library.RemoveBook(book.ISBN);

            // Assert
            Assert.IsTrue(result, "RemoveBook should return true when the book is in stock and not loaned out.");
        }

        // Test Case 5
        [TestMethod]
        public void RemoveBook_WhenBookIsBorrowed_ShouldReturnFalse()
        {
            // Arrange
            var library = new LibrarySystem();
            var book = library.SearchByISBN("9780451524935");
            book.IsBorrowed = true;

            int countBefore = library.GetAllBooks().Count; // Counts the total amount of books in the library

            // Act
            bool result = library.RemoveBook(book.ISBN);
            int countAfter = library.GetAllBooks().Count;

            // Assert
            Assert.AreEqual(countBefore, countAfter, "Borrowed book should not be removed from the list.");
            Assert.IsFalse(result, "RemoveBook should return false when trying to remove a borrowed book.");
        }

        // Test Case 6
        [TestMethod]
        public void RemoveBook_WhenBookDoesNotExist_ShouldReturnFalse()
        {
            // Arrange
            var library = new LibrarySystem();
            var book = new Book("Test Title",  "Test Author", "5479406656416", 2020);

            string nonExistentISBN = "0000000000"; // ISBN that does not exist in the library
            // Act
            bool result = library.RemoveBook(nonExistentISBN);
            // Assert
            Assert.IsFalse(result, "RemoveBook should return false when trying to remove a book that does not exist.");
        }

        // Test Case 7
        [TestMethod]
        public void SearchBook_WithExactISBN_ShouldReturnNull()
        {
            // Arrange
            var library = new LibrarySystem();
            var book = new Book("Test Title", "Test Author", "5479406656416", 2020);
            library.AddBook(book);

            // Act
            var result = library.SearchByISBN("5479406656416");

            // Assert
            Assert.IsNotNull(result, "SearchByISBN should return a book when the ISBN exists.");
        }

        // Test Case 8
        [TestMethod]
        public void SearchBook_WithTheFullTitle_ShouldReturnNull()
        {
            // Arrange
            var library = new LibrarySystem();
            var book = new Book("Test Title", "Test Author", "5479406656416", 2020);
            library.AddBook(book);

            // Act
            var result = library.SearchByTitle("Test Title");

            // Assert
            Assert.IsNotNull(result, "SearchByTitle should return a book if the title exists.");
        }

        // Test Case 9
        [TestMethod]
        public void SearchBook_WithPartOfTheTitle_ShouldReturnNull()
        {
            // Arrange
            var library = new LibrarySystem();
            var book = new Book("Test Title", "Test Author", "5479406656416", 2020);
            library.AddBook(book);

            // Act
            var result = library.SearchByTitle("Te");

            // Assert
            Assert.IsNotNull(result, "SearchByTitle should return a book when the title or part of the title exists.");
        }

        // Test Case 10
        [TestMethod]
        public void SearchBook_WithAuthorFullNameOrPartOfTheName_ShouldReturnNull()
        {
            // Arrange
            var library = new LibrarySystem();
            var book = new Book("Test Title", "Test Author", "5479406656416", 2020);
            library.AddBook(book);

            // Act
            var resultFullName = library.SearchByAuthor("Test Author"); // Search by full author name
            var resultPartialName = library.SearchByAuthor("Test"); // Search by part of the author name

            // Assert
            Assert.IsNotNull(resultFullName, "SearchByAuthor should return a book when the full author name exists.");
            Assert.IsNotNull(resultPartialName, "SearchByAuthor should return a book when part of the author name exists.");
        }

        // Test Case 11
        [TestMethod]
        public void SearchBook_WithAuthorNameWithUpperOrLowerCase_ShouldReturnNotNull()
        {
            // Arrange
            var library = new LibrarySystem();
            var book = new Book("Test Title", "test author", "5479406656416", 2020);
            library.AddBook(book);

            // Act
            var resultPartialUpperCase = library.SearchByAuthor("TEST author"); // Search with mixed-case author name
            var resultUpperCase = library.SearchByAuthor("TEST AUTHOR"); // Search with all upper-case author name
            var resultOnlyLowerCase = library.SearchByAuthor("test"); // Search by part of the author name in lower case

            // Assert
            Assert.AreEqual(1, resultPartialUpperCase.Count, "SearchByAuthor should return one book when using mixed-case author name.");
            Assert.AreEqual(1, resultUpperCase.Count, "SearchByAuthor should return one book when using all upper-case author name.");
            Assert.AreEqual(1, resultOnlyLowerCase.Count, "SearchByAuthor should return one book when using part of the author name in lower case.");
        }

        // Test Case 12
        [TestMethod]
        public void SearchBook_WithNoAutherNameNoIsbnNoTitle_ShouldReturnNull()
        {
            // Arrange
            var library = new LibrarySystem();
            var book = new Book("Test Title", "test author", "5479406656416", 2020);
            library.AddBook(book);

            // Act
            var resultWithWrongAuthor = library.SearchByAuthor("Petter");
            var resultWithTitle = library.SearchByTitle("Petter");
            var resultWithWrongISBN = library.SearchByISBN("2222222222222");

            // Assert
            Assert.AreEqual(0, resultWithWrongAuthor.Count, "SearchByAuthor should return no results when the author does not match.");
            Assert.AreEqual(0, resultWithTitle.Count, "SearchByTitle should return no results when the title does not match.");
            Assert.IsNull(resultWithWrongISBN, "SearchByISBN should return null when the ISBN does not match any book.");
        }


        //Utlåningssystem
        // Test Case 13
        [TestMethod]
        public void BorrowBook_WhenBookIsInStock_ShouldReturnTrue()
        {
            // Arrange
            var library = new LibrarySystem();
            var book = new Book("Test Title","test author", "5479406656416", 2020);
            book.IsBorrowed = false; // Ensuring the book is available
            library.AddBook(book); // Add the book to the library system

            int countBefore = library.GetAllBooks().Count; // Counts the total amount of books in the library

            // Act
            bool result = library.BorrowBook(book.ISBN);
            int countAfter = library.GetAllBooks().Count;

            // Assert
            Assert.IsTrue(result, "Book should return true when borrowing an available book.");
            Assert.AreEqual(countBefore, countAfter, "The number of books should remain the same after borrowing.");
        }

        // Test case 14
        [TestMethod]
        public void BorrowBook_WhenBookIsNotInStock_ShouldReturnFalse()
        {
            // Arrange
            var library = new LibrarySystem();
            var book = new Book("Test Title", "test author", "5479406656416", 2020);
            book.IsBorrowed = true; // Ensuring the book is not in stock
            library.AddBook(book); // Add the book to the library system
            int countBefore = library.GetAllBooks().Count; // Counts the total amount of books in the library

            // Act
            bool result = library.BorrowBook(book.ISBN);
            int countAfter = library.GetAllBooks().Count;

            // Assert
            Assert.IsFalse(result, "Book should return false when borrowing an unavailable book.");
            Assert.AreEqual(countBefore, countAfter, "The number of books should remain the same after borrowing.");
        }

        // Test case 15
        [TestMethod]
        public void BorrowBook_WhenBookDoesNotExist_ShouldReturnFalse()
        {
            // Arrange
            var library = new LibrarySystem();
            var ISBN = "0000000000"; // ISBN that does not exist in the library

            // Act
            int countBefore = library.GetAllBooks().Count; // Counts the total amount of books in the library
            bool result = library.BorrowBook(ISBN);
            int countAfter = library.GetAllBooks().Count; // Counts the total amount of books in the library after borrowing

            // Assert
            Assert.IsFalse(result, "BorrowBook should return false when the book does not exist in the library.");
            Assert.AreEqual(countBefore, countAfter, "The number of books should remain the same after attempting to borrow a non-existent book.");
        }

        // Test case 16
        [TestMethod]
        public void ReturnBook_WhenBookIsBorrowed_ShouldReturnTrue()
        {
            // Arrange
            var library = new LibrarySystem();
            var book = new Book("Test Title", "test author", "5479406656416", 2020);
            book.IsBorrowed = true;
            book.BorrowDate = DateTime.Now; // Simulera att boken har ett utlåningsdatum
            library.AddBook(book);

            int countBefore = library.GetAllBooks().Count;

            // Act
            bool result = library.ReturnBook(book.ISBN);
            int countAfter = library.GetAllBooks().Count;

            // Assert
            Assert.IsTrue(result, "ReturnBook should return true when successfully returning a borrowed book.");
            Assert.AreEqual(countBefore, countAfter, "The number of books should remain the same after returning the book.");
            Assert.IsFalse(book.IsBorrowed, "The book should no longer be marked as borrowed after being returned.");
            Assert.IsNull(book.BorrowDate, "BorrowDate should be reset to null when returning the book.");
        }

        // Test case 17
        [TestMethod]
        public void ReturnBook_WhenBookIsNotBorrowed_ShouldReturnFalse()
        {
            // Arrange
            var library = new LibrarySystem();
            var book = new Book("Test Title", "test author", "5479406656416", 2020);
            book.IsBorrowed = false; // not borrowed book
            library.AddBook(book);

            int countBefore = library.GetAllBooks().Count; // Counts the total amount of books in the library before returning

            // Act
            bool result = library.ReturnBook(book.ISBN); // Try returning the book using its ISBN
            int countAfter = library.GetAllBooks().Count; // Counts the total amount of books in the library after returning

            // Assert
            Assert.IsFalse(result, "ReturnBook should return false when the book is not borrowed.");
            Assert.AreEqual(countBefore, countAfter, "The number of books should remain the same after attempting to return a book that was not borrowed.");
            Assert.IsFalse(book.IsBorrowed, "The book should remain marked as not borrowed since it was not borrowed in the first place.");
        }

        // Test case 18
        [TestMethod]
        public void ReturnBook_WhenBookDoesNotExist_ShouldReturnFalseAndHandleError()
        {
            // Arrange
            var library = new LibrarySystem();
            var book = new Book("Test Title", "Test Author", "5479406656416", 2020);

            string nonExistentISBN = "0000000000"; // ISBN that does not exist in the library

            // Act
            bool result = library.ReturnBook(nonExistentISBN);

            // Assert
            Assert.IsFalse(result, "ReturnBook should return false when trying to return a book that does not exist.");
        }

        // Test case 19
        [TestMethod]
        public void IsBookOverdue_WhenBookIsWithinLoanPeriod_ShouldReturnFalse()
        {
            // Arrange
            var library = new LibrarySystem();
            var book = new Book("Test Title", "Test Author", "5479406656416", 2020);
            int loanPeriodDays = 14;

            // Set the book as borrowed and within the loan period
            book.IsBorrowed = true;
            book.BorrowDate = DateTime.Now.AddDays(-7); // Book borrowed 7 days ago (within the loan period)

            library.AddBook(book);

            // Act
            bool isOverdue = library.IsBookOverdue(book.ISBN, loanPeriodDays); // Check if the book is overdue

            // Assert
            Assert.IsFalse(isOverdue, "The book should not be considered overdue when it is within the loan period.");
        }


        // Test case 20
        [TestMethod]
        public void CalculateLateFee_WhenBookIs5DaysLate_ShouldReturnCorrectFee()
        {
            // Arrange
            var library = new LibrarySystem();
            var book = new Book("Test Title", "Test Author", "5479406656416", 2020);
            book.IsBorrowed = true;
            book.BorrowDate = DateTime.Now.AddDays(-19); // Borrowed 19 days ago
            library.AddBook(book);

            int loanPeriodDays = 14;
            int daysLate = 19 - loanPeriodDays;

            // Act
            decimal fee = library.CalculateLateFee(book.ISBN, daysLate);

            // Assert
            Assert.AreEqual(2.5m, fee, "The late fee is calculated wrong");
        }


        // Test case 21
        [TestMethod]
        public void IsBookOverdue_WhenBookIsReturnedInTime_ShouldReturnFalse()
        {
            // Arrange
            var library = new LibrarySystem();
            var book = new Book("Test Title", "Test Author", "5479406656416", 2020);
            int loanPeriodDays = 14;
            // Set the book as borrowed within the loan period
            book.IsBorrowed = true;
            book.BorrowDate = DateTime.Now.AddDays(-5); // Book borrowed 5 days ago
            library.AddBook(book);

            // Act
            bool isOverdue = library.IsBookOverdue(book.ISBN, loanPeriodDays); // Check if the book is overdue

            // Assert
            Assert.IsFalse(isOverdue, "The book should not be overdue if it is within the loan period.");
        }
    }
}