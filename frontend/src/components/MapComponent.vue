<script>
//TODO: Finish components
export default {
    data() {
        
    },
    props: {
        image_url: String,
        name_: String, 
        weight: Number
    },
    emits: ['onRemove']
}
</script>
<style>

.container {
    position: relative;
}

#image {
    height: 200px;
    display: block;
}

#name-overlay {
    display: flex;
    margin: 0px;
    height: 40px;
    background-color: rgb(255, 162, 0);
    justify-content: center;
    align-items: center;
}

.overlay {
    height: 200px;
    position: absolute;
    top: 0;
    bottom: 0;
    left: 0;
    right: 0;
    opacity: 0;
    transition: .5s ease;
    background-color: rgb(255, 162, 0);
}

.container:hover .overlay {
    opacity: 1;
}
</style>
<template>
    <div class="container">
        <img id="image" :src="this.image_url"/>
        <span class="overlay">Here will be options to remove maps. Also the name
            and rarity</span>
            <div id="name-overlay">
                <span>{{this.name_}}</span>
                <span>{{this.weight}}</span>
            </div>
        </div>

</template>