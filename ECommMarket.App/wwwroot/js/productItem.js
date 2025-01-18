const quantityInput = document.getElementById('quantity-input');
function changeImage(imageUrl) {
    const mainImage = document.getElementById('mainImage');
    mainImage.src = imageUrl;
}

function getQuantityValue() {
    return quantityInput.value;
}