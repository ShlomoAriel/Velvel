$(document).ready(function () {

    $('#send-line button').on('click', function () {
        var Comment= {};
        Comment.CommentGroupId = 1;
        Comment.UserId = 2;
        Comment.Content = $("#comment-content").value;
        $.post("/Changes/AddComment",Comment);
    });
});