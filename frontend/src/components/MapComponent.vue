<script>
export default {
    props: {
        image_url: String,
        name_: String, 
        weight: Number,
        id: String,
        played: Boolean,
        active_color: String
    },
    emits: ['mapRemoved', 'mapToggle', 'startMap'],
    methods: {
        getUrl() {
            return "https://steamcommunity.com/sharedfiles/filedetails/?id=" + this.id;
        },

        getPlayedText() {
            if(this.played) return "Not Played?"
            return "Already Played?"
        }
    }
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
    background-color: v-bind(active_color);
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
    background-color: rgb(204,186,124);
    display: flex;
    flex-direction: column;
}

.overlay >* {
    text-align: center;
}

.buttons {
    display: flex;
    height: 95%;
    background-color: aqua;
}

.buttons >* {
    flex: 1;
    display: flex;
    justify-content: center;
    align-items: center;
    flex-direction: column;
    row-gap: 10px;
}

.info {
    height: 5%;
    font-size: xx-small;
    text-align: right;
    margin: 2px;
    color: grey;
}

#remove {
    background-color: rgb(222,155,53);

}

#played {
    background-color: rgb(93,121,174);
}

#playmap {
    background-color: #109856;
}

.button-overlay {
    cursor: pointer;
}

.nonselectable {
    user-select: none;
}

.container:hover .overlay {
    opacity: 1;
}
</style>
<template>
    <div class="container">
        <img id="image" :src="this.image_url"/>
        <div class="overlay">
            <div class="buttons">
                <div id="remove" class="button-overlay" @click="this.$emit('mapRemoved', this.id)">
                    <font-awesome-icon :icon="['fas', 'trash']"
                    size="5x"/>
                    <span class="nonselectable">Remove Map</span>
                </div>
                <div id="played" class="button-overlay" @click="this.$emit('mapToggle', this.id)">
                    <font-awesome-icon :icon="['fas', 'gamepad']"
                    size="5x" />
                    <span class="nonselectable">{{ getPlayedText() }}</span>
                </div>
                <div id="playmap" class="button-overlay" @click="this.$emit('startMap', this.id)">
                    <font-awesome-icon :icon="['fas', 'play']"
                    size="5x" />
                    <span class="nonselectable">Play Map</span>
                </div>
            </div>
            <div class="info">
                Id: {{ this.id }}, Weight: {{ this.weight }}
            </div>
        </div>
        <div id="name-overlay">
            <a id="url" :href="getUrl()" target="_blank" rel="noopener
            noreferrer">{{ this.name_ }}</a>
        </div>
    </div>
</template>