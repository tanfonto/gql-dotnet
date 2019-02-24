const webpack = require('webpack'),
    CopyWebpackPlugin = require('copy-webpack-plugin'),
    PATH = require('path');

module.exports = {
    entry: {
        'bundle': './app/index.js'
    },
    output: {
        path: PATH.join(__dirname, '/public'),
        filename: '[name].js'
    },
    devtool: 'source-map',
    resolve: {
        extensions: ['.js', '.json']
    },
    module: {
        loaders: [
          { 
              test: /\.js/, 
              loader: 'babel-loader', 
              exclude: /node_modules/,
              query: {
                  presets: ['react']
              } 
          },
          { 
              test: /\.css$/, 
              loaders: ['style-loader', 'css-loader'] 
          }
        ]
    },
    plugins: [
        new CopyWebpackPlugin([
            { from: 'app/index.html'}
        ])
    ]
};