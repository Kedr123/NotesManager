const { defineConfig } = require('@vue/cli-service')
module.exports = defineConfig({
  transpileDependencies: true,
  devServer:{
    https:false,
    headers: {
      'Access-Control-Allow-Origin': '*',
      'Cache-Control': null,
      'X-Requested-With': null,
      'Content-Type': 'text/html'
    }
  },

})
