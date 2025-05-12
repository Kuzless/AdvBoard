function popSubCategories() {
    var categoryId = document.getElementById("CategoryId").value;
    var subCategorySelect = document.getElementById("SubCategoryId");

    subCategorySelect.innerHTML = "";
    var subCategories = JSON.parse(document.getElementById("SubCategoryData").value);

    var filteredSubCategories = subCategories.filter(function (subCategory) {
        return subCategory.CategoryId == categoryId;
    });

    filteredSubCategories.forEach(function (subCategory) {
        var option = document.createElement("option");
        option.value = subCategory.Id;
        option.textContent = subCategory.Name;
        subCategorySelect.appendChild(option);
    });
}

document.addEventListener("DOMContentLoaded", function () {
    popSubCategories();
});
