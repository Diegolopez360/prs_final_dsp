$(document).ready(function () {
    $("input[type='file']").change(function () {
        console.log("CHOOSE FILE...");
        console.log(this);
        if (this.files && this.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $("#dataImageUpload").empty();
                $("#dataImageUpload").append($("<img/>", { src: e.target.result, class: 'w-75 h-auto rounded drop-shadow' }));
            };
            reader.readAsDataURL(this.files[0]);
        }
    });
});