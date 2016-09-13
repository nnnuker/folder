var storage = function() {
    var add = function(item) {
        window.localStorage.setItem("item" + item.ToDoId, JSON.stringify(item));
    }

    var remove = function(item) {
        window.localStorage.removeItem("item" + item.ToDoId);
    }

    var getAll = function () {
        var archive = []; // Notice change here
        var keys = Object.keys(localStorage);
        var i = keys.length - 1;

        while (i) {
            archive[i] = JSON.parse(localStorage.getItem(keys[i]));
            i--;
        }

        return archive;
    }

    var set = function(items) {
        //        var items = JSON.parse(jsonItems);
        if (items.length !== 0) {
            $.each(items, function (i, item) {
                add("item" + item.ToDoId, JSON.stringify(item));
            });
        }
    }

    var update = function(item) {
        window.localStorage.removeItem("item"+item.ToDoId);
        window.localStorage.setItem("item" + item.ToDoId, JSON.stringify(item));
    }

    return {
        add: add,
        remove: remove,
        getAll: getAll,
        set: set,
        update:update
    };
}();

var tasksManager = function () {
    // appends a row to the tasks table.
    // @parentSelector: selector to append a row to.
    // @obj: task object to append.
    var appendRow = function(parentSelector, obj) {
        var tr = $("<tr data-id='" + obj.ToDoId + "'></tr>");
        tr.append("<td><input type='checkbox' class='completed' " + (obj.IsCompleted ? "checked" : "") + "/></td>");
        tr.append("<td class='name' >" + obj.Name + "</td>");
        tr.append("<td><input type='button' class='delete-button' value='Delete' /></td>");
        $(parentSelector).append(tr);
    }
    var removeRow = function(itemId) {
        $("tr").find("[data-id='" + itemId + "']").remove();
        
    }
    var load = function() {
        return $.getJSON("/api/todos");
    };
  

    // adds all tasks as rows (deletes all rows before).
    // @parentSelector: selector to append a row to.
    // @tasks: array of tasks to append.
    var displayTasks = function (parentSelector, tasks) {
        $(parentSelector).empty();
        $.each(tasks, function (i, item) {
            appendRow(parentSelector, item);
        });
    };

    // starts loading tasks from server.
    // @returns a promise.
    var loadTasks = function () {
        
        if (storage.getAll().length === 0) {
            debugger;
//TODO: stand by func ready
            storage.set($.getJSON("/api/todos"));
        }
        var result = storage.getAll();
        return result;
    };




    // starts creating a task on the server.
    // @isCompleted: indicates if new task should be completed.

    // @name: name of new task.
    // @return a promise.
    var createTask = function (isCompleted, name) {
//        var id = getRandomInt(1, 1000);

        var obj = {
            ToDoId: 0,
            IsCompleted: isCompleted,
            Name: name
        };

        storage.add(obj);

        appendRow("#tasks > tbody", obj);

        return $.post("/api/todos", {
            IsCompleted: isCompleted,
            Name: name
        });
    };


    // starts updating a task on the server.
    // @id: id of the task to update.
    // @isCompleted: indicates if the task should be completed.
    // @name: name of the task.
    // @return a promise.
    var updateTask = function(id, isCompleted, name) {
        return $.ajax(
        {
            url: "/api/todos",
            type: "PUT",
            contentType: 'application/json',
            data: JSON.stringify({
                ToDoId: id,
                IsCompleted: isCompleted,
                Name: name
            })
        });
    };

    // starts deleting a task on the server.
    // @taskId: id of the task to delete.
    // @return a promise.
    var deleteTask = function (taskId) {
        storage.remove(taskId);
        removeRow(taskId);
        return $.ajax({
            url: "/api/todos/" + taskId,
            type: "DELETE"
        });
    };

    // returns public interface of task manager.
    return {
        loadTasks: loadTasks,
        displayTasks: displayTasks,
        createTask: createTask,
        deleteTask: deleteTask,
        updateTask: updateTask,
        load:load
    };
}();


$(function () {
    // add new task button click handler
    $("#newCreate").click(function() {
        var isCompleted = $('#newCompleted')[0].checked;
        var name = $('#newName')[0].value;

        tasksManager.createTask(isCompleted, name)
            .then(tasksManager.load)
            .done(function(tasks) {
                var localTasks = storage.getAll();
                for (var i = 0; i < tasks.length; i++) {
                    for (var j = 0; j < localTasks.length; j++) {
                        if (tasks[i].Name === localTasks[j].Name && localTasks[j].ToDoId === 0) {
                            localTasks[j].ToDoId = tasks[i].ToDoId;
                            storage.update(localTasks[j]);
                        }
                    }
                }
            });
    });


    // bind update task checkbox click handler
    $("#tasks > tbody").on('change', '.completed', function () {
        var tr = $(this).parent().parent();
        var taskId = tr.attr("data-id");
        var isCompleted = tr.find('.completed')[0].checked;
        var name = tr.find('.name').text();
        
        tasksManager.updateTask(taskId, isCompleted, name)
            .then(tasksManager.loadTasks)
            .done(function (tasks) {
                tasksManager.displayTasks("#tasks > tbody", tasks);
            });
    });

    // bind delete button click for future rows
    $('#tasks > tbody').on('click', '.delete-button', function() {
        var taskId = $(this).parent().parent().attr("data-id");
        this.remove();
        tasksManager.deleteTask(taskId);
//            .then(tasksManager.loadTasks)
//            .done(function(tasks) {
//                tasksManager.displayTasks("#tasks > tbody", tasks);
//            });
    });

    // load all tasks on startup
    var elements = tasksManager.loadTasks();
    tasksManager.displayTasks("#tasks > tbody", elements);
 });