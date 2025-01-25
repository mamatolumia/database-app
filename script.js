
// Simulated database
let database = [];
let idCounter = 1;

// Function to render the database entries to the table
function renderEntries() {
    const entriesTable = document.getElementById("entries");
    entriesTable.innerHTML = "";

    database.forEach(entry => {
        const row = document.createElement("tr");

        row.innerHTML = `
            <td>${entry.id}</td>
            <td>${entry.name}</td>
            <td>${entry.email}</td>
            <td>${entry.age}</td>
            <td>
                <button class="action-btn edit" onclick="editEntry(${entry.id})">Edit</button>
                <button class="action-btn delete" onclick="deleteEntry(${entry.id})">Delete</button>
            </td>
        `;

        entriesTable.appendChild(row);
    });
}

// Function to add a new entry
document.getElementById("entry-form").addEventListener("submit", function (event) {
    event.preventDefault();

    const name = document.getElementById("name").value;
    const email = document.getElementById("email").value;
    const age = parseInt(document.getElementById("age").value);

    const newEntry = { id: idCounter++, name, email, age };
    database.push(newEntry);

    renderEntries();

    alert("Entry added successfully!");
    event.target.reset();
});

// Function to delete an entry
function deleteEntry(id) {
    database = database.filter(entry => entry.id !== id);
    renderEntries();
    alert("Entry deleted successfully!");
}

// Function to edit an entry (simple alert for now)
function editEntry(id) {
    const entry = database.find(entry => entry.id === id);
    alert(`Editing entry: ${entry.name}`);
}

// Initial render
renderEntries();
