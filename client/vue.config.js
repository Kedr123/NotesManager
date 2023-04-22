const {defineConfig} = require('@vue/cli-service');
const fs = require('fs');
module.exports = defineConfig({
    transpileDependencies: true,
    devServer: {
        https: {
            key: fs.readFileSync("./certs/localhost+2-key.pem"),
            cert: fs.readFileSync('./certs/localhost+2.pem'),
            url:"https://127.0.0.1:8080",
        },
    }

})
