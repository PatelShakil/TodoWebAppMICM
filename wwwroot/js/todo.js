$(document).ready(function () {
    let storageType = "TextFile";

    function loadTodos() {
        $.ajax({
            url: 'https://localhost:7290/api/Todo?storageType=' + storageType,
            method: 'GET',
            success: function (data) {
                $('#todoList').empty();
                $('#todoCount').text(data.length);
                data.forEach(todo => {
                    $('#todoList').append(`
                        <li class="list-group-item d-flex justify-content-between align-items-center">
                            ${todo.title}
                            <div>
                                <button class="btn btn-sm btn-success checkTodoBtn" data-id="${todo.id}">✔</button>
                                <button class="btn btn-sm btn-danger deleteTodoBtn" data-id="${todo.id}">✖</button>
                            </div>
                        </li>`);
                });
            }
        });
    }

    // Change storage type
    $('input[name="storageType"]').on('change', function () {
        storageType = $(this).val();
        $('#currentStorage').text(storageType);
        loadTodos();
    });

    // Initial load
    loadTodos();
});