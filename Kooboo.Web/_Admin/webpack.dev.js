const path = require('path')
const VueLoaderPlugin = require('vue-loader/lib/plugin');
const HtmlWebpackPlugin = require('html-webpack-plugin');

module.exports = {
  mode: 'development',
  context: path.resolve('Scripts/vue/components'),
  entry: {
    table: './kbTable/dev.js'
  },
  devtool: 'inline-source-map',
  devServer: {
  },
  plugins: [
    new VueLoaderPlugin(),
    new HtmlWebpackPlugin({
      filename: 'dev.html',
      template: path.resolve(__dirname, 'dev.html'),
      inject: true,
      chunks: ['table']
    }),
  ],
  output: {
    filename: '[name].js',
    path: path.resolve(__dirname, './dist')
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