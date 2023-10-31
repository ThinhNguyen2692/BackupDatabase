$(document).ready(function () {
    $(".Day-ActionType").change(function () {
        var IsCheckTime = $(this).val();
        if (IsCheckTime == "True") {
            $(".EndTime-Only").show()
        } else {
            $(".EndTime-Only").css("display", "none")
        }
       
    });


    $(".job-item").on('click', function (event) {
        
    });
    $("#editButton").click(function () {
        // Lấy nội dung hiện tại của label
        var currentName = $("#nameLabel").text()||'';
        console.log(1)
        // Tạo một input để chỉnh sửa tên
        var inputElement = $(`<input type="text" class="form-control" style=" width: calc(100% - 30px); ">`)
            .val(currentName.trim());

        // Thay thế label bằng input để chỉnh sửa
        $("#nameLabel").replaceWith(inputElement);

        // Focus vào input để người dùng có thể sửa tên
        inputElement.focus();

        // Bắt sự kiện khi người dùng kết thúc chỉnh sửa
        inputElement.blur(function () {
            // Lấy giá trị mới từ input
            var newName = inputElement.val();

            // Tạo lại label với tên mới
            var newLabel = $("<label>")
                .attr("id", "nameLabel")
                .text(newName);
            // Thay thế input bằng label mới
            inputElement.replaceWith(newLabel);
            $('#BackupName').val(newName);
        });
    });

    $(".select-Occurs").change(function () {
        $(".Occurs-Group").css("display", "none")
        var name = $(this).val() == 4 ? "Day" : $(this).val() == 8 ? "Weekly" : "Monthly";
        var classdisplay = "." + name + "-Group"
        $(classdisplay).show()
    });
    $(".select-Day-radio").click(() => {
        $(".select-Day").prop("disabled", false);
        $(".select-The").prop("disabled", true);
    })
    $(".select-The-radio").click(() => {
        $(".select-Day").prop("disabled", true);
        $(".select-The").prop("disabled", false);
    })
    let display = 0;
    $(".icon-email").click(() => {
        if (display == 0) {
            $(".config-email").show()
            display = 1;
        } else {
            $(".config-email").css("display", "none")
            display = 0;
        }
    })
});
