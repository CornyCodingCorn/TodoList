## 📖 ABOUT

A simple todo list app that is cross-platform and simple but still has enough feature for managing daily tasks.</br>
This app aims to fix the lack of app that fit right in the middle of being features rich and just a simple app that
everyone does as a tutorial (I actually don't know, don't quote me ¯\\\_(ツ)_/¯ ). </br>

## 🎯 TO DO

### Must have:

- Saving data in yaml file format
- A dashboard to show compiled data
- Keep track of how many task has been done
- Total working time
- Compile data
- Have test project

### Nice to have:

- Reminder to get some rest
- Prepare task for tomorrow
- A music player that play local songs
- Allow changing themes
- Context menu for task item
- Sort task
- Allow user to datetime format
- Allow user to set time before moving to archive
- Mark how many task was done in the datetime picker

## 🕦 DOING

### Must have:

### Nice to have:


## ✅ DONE

### Must have:

- Add DI to the project and use it for all viewmodel that will only have 1 instance
- Tasks with timer to keep track
- Hamburger menu for switching windows
- Create custom expander -> Change the expander to look much better
- Remove expander border(change it to the same color), reduce height and add animation for open and close
- A menu to add task
- Split popup presenter to viewmodel and view
- Make show dialog an async Task
- When program is opened, all task will be checked to trigger archiving countdown if it's completed
- Add task to achieve archive when done and leave there for 5 minutes and record done datetime
- Archive tasks will be shown by date

### Nice to have:

- Confirmation popup
- Change button to Icons and tool tips (tool tips flickering on wayland so can't do it)
- Change button when window size to too small for icon and text
- Add animation for task deletion

### Task item:

- Change animation data type to correct one because C# doesn't automatically type cast object to type (Done)
- Store time in milliseconds
- Icon to show the status
- A name description
- Click will show a dropdown
- The dropdown will show a full info
- Style the item dropdown
- Update button will update the status
- Change buttons base on state
- Split the task item code into view model and control
- Textblock and then render as markdown
- Edit button will allow editing
