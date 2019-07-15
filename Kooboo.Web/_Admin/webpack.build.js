const path = require('path')
const VueLoaderPlugin = require('vue-loader/lib/plugin');

module.exports = {
  mode: 'development',
  context: path.resolve(__dirname, 'Scripts/vue/components'),
  entry: {
    kbTab:'./kbTab/index.js',
    kbForm:'./kbForm/index.js',
    kbPopup:'./kbPopup/index.js',
    kbTable: './kbTable/index.js'
  },
  plugins: [
    new VueLoaderPlugin()
  ],
  output: {
    filename: '[name].js',
    path: path.resolve(__dirname, 'Scripts/vue/components')
  },
  resolve: {
    extensions: ['.js', '.vue', '.json'],
    alias: {
      vue: 'vue/dist/vue.js'
    }
  },
  module: {
    rules: [{
      test: /\.vue$/,
      loader: 'vue-loader',
      exclude: /node_modules/
    },
    {
      test: /\.js$/,
      loader: 'babel-loader',
      exclude: /node_modules/
    }]
  },
  externals: {
    vue: 'Vue'
  }
};