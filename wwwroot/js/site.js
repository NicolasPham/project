const switchVideo = (key) => {
    let video = document.getElementById("videoName");
    video.src = key;
    
    let ddl = document.getElementById("video_DDL");
    ddl.blur();
}