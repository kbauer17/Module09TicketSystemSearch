using NLog;

// See https://aka.ms/new-console-template for more information
string path = Directory.GetCurrentDirectory() + "//nlog.config";

// create instance of Logger
var logger = LogManager.LoadConfiguration(path).GetCurrentClassLogger();

// designate the file to read/write to
string ticketFilePath = Directory.GetCurrentDirectory() + "//tickets.csv";
string enhancementFilePath = Directory.GetCurrentDirectory() + "//enhancements.csv";
string taskFilePath = Directory.GetCurrentDirectory() + "//tasks.csv";

logger.Info("Program started");

TicketFile ticketFile = new TicketFile(ticketFilePath);
EnhancementFile enhancementFile = new EnhancementFile(enhancementFilePath);
TaskFile taskFile = new TaskFile(taskFilePath);

string choice = "";
string chosenSubClass = "";
do
{
  // display choices to user
  Console.WriteLine("1) Add Item");
  Console.WriteLine("2) Display All Items");
  Console.WriteLine("Enter to quit");

  // input selection
  choice = Console.ReadLine();
  logger.Info("User choice: {Choice}\n", choice);
  
  if (choice == "1")
  {
    // Add ticket
    HelpTicket ticket = new HelpTicket();
    Enhancement enhancement = new Enhancement();
    Task task = new Task();

    // Ask which type of ticket to add
    Console.WriteLine("Select which type of item to add:");
    Console.WriteLine("1) Add Help Ticket");
    Console.WriteLine("2) Add Enhancement");
    Console.WriteLine("3) Add Task");

    // input selection
    chosenSubClass = Console.ReadLine();
    logger.Info("User choice: {chosenSubClass}", chosenSubClass);

    // Obtain input for the common fields
    
    // prompt for a item id number
    Console.WriteLine("Enter a Item Number: ");
    // save the item id number
    ticket.ticketId = UInt64.Parse(Console.ReadLine());
    // prompt for item Summary
    Console.WriteLine("Enter a Summary for the item:  ");
    // save the item Summary
    ticket.summary = Console.ReadLine();
    // prompt for ticket status
    Console.WriteLine("Enter the item status (open/closed):");
    // save the item status
    ticket.status = Console.ReadLine();
    // prompt for priority
    Console.WriteLine("Enter the item priority (high/low):");
    // save the item priority
    ticket.priority = Console.ReadLine();
    // prompt for the name of the submitter
    Console.WriteLine("Enter the name of the person submitting the item:");
    // save the name of the submitter
    ticket.submitter = Console.ReadLine();
    // prompt for the name of the assignee
    Console.WriteLine("Enter the name of the person to whom the item is assigned:");
    // save the name of the assignee
    ticket.assigned = Console.ReadLine();
    // prompt for the name of the watcher
    Console.WriteLine("Enter the name of the person watching the item:");
    // save the name of the watcher
    ticket.watcher = Console.ReadLine();

    // populate the fields specific to each subclass
    switch(chosenSubClass){
      case "1": // Help Ticket
        // prompt for a ticket severity
        Console.WriteLine("Enter the Severity (1 - 10): ");
        // save the Ticket id number
        ticket.severity = UInt64.Parse(Console.ReadLine());
        // add ticket
        ticketFile.AddTicket(ticket);

        break;
      case "2": // Enhancement
        enhancement.ticketId = ticket.ticketId;
        enhancement.summary = ticket.summary;
        enhancement.status = ticket.status;
        enhancement.priority = ticket.priority;
        enhancement.submitter = ticket.submitter;
        enhancement.assigned = ticket.assigned;
        enhancement.watcher = ticket.watcher;
      

        // prompt for if software is needed
        Console.WriteLine("Is software needed? (Y/N): ");
        enhancement.software = Console.ReadLine();
        // prompt for cost
        Console.WriteLine("Enter the cost: ");
        enhancement.cost = Console.ReadLine();
        // prompt for the reason
        Console.WriteLine("Enter the reason software is needed: ");
        enhancement.reason = Console.ReadLine();
        // prompt for the estimate
        Console.WriteLine("Enter the estimated number of weeks needed: ");
        enhancement.estimate = UInt64.Parse(Console.ReadLine());

        // add enhancement to enhancement list file
        enhancementFile.AddEnhancement(enhancement);
        break;
      case "3": // Task
        task.ticketId = ticket.ticketId;
        task.summary = ticket.summary;
        task.status = ticket.status;
        task.priority = ticket.priority;
        task.submitter = ticket.submitter;
        task.assigned = ticket.assigned;
        task.watcher = ticket.watcher;

        // prompt for project name
        Console.WriteLine("Please enter the Project Name: ");
        task.projectName = Console.ReadLine();
        // prompt for due date
        Console.WriteLine("Please enter the due date (mm/dd/yyyy): ");
        task.dueDate = Console.ReadLine();

        // add task to task list file
        taskFile.AddTask(task);
        break;
    }

  } else if (choice == "2")
  {
    // Ask which type of item to display
    Console.WriteLine("Select which type of items to display:");
    Console.WriteLine("1) Display Help Tickets");
    Console.WriteLine("2) Display Enhancements");
    Console.WriteLine("3) Display Tasks");

    // input selection
    chosenSubClass = Console.ReadLine();
    logger.Info("User choice: {chosenSubClass}\n", chosenSubClass);

    // Display All Itemss
    switch(chosenSubClass){
      case "1": // Help Ticket
        foreach(HelpTicket t in ticketFile.Tickets)
        {
          Console.WriteLine(t.Display());
        }
        break;
      case "2": // Enhancement
        foreach(Enhancement e in enhancementFile.Enhancements)
        {
          Console.WriteLine(e.Display());
        }
        break;
      case "3": // Task
        foreach(Task t in taskFile.Tasks)
        {
          Console.WriteLine(t.Display());
        }
        break;
    }

    
  }

} while (choice == "1" || choice == "2");


logger.Info("Program ended");
