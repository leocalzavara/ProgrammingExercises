#!/usr/bin/python
# Copyright 2010 Google Inc.
# Licensed under the Apache License, Version 2.0
# http://www.apache.org/licenses/LICENSE-2.0

# Google's Python Class
# http://code.google.com/edu/languages/google-python-class/

# Leonardo P. Calzavara
# https://github.com/leocalzavara

import os
import re
import sys
import urllib
import urlparse

"""Logpuzzle exercise
Given an apache logfile, find the puzzle urls and download the images.

Here's what a puzzle url looks like:
10.254.254.28 - - [06/Aug/2007:00:13:48 -0700] "GET /~foo/puzzle-bar-aaab.jpg HTTP/1.0" 302 528 "-" "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.1.6) Gecko/20070725 Firefox/2.0.0.6"
"""


def read_urls(filename):
  """Returns a list of the puzzle urls from the given log file,
  extracting the hostname from the filename itself.
  Screens out duplicate urls and returns the urls sorted into
  increasing order."""
  # +++your code here+++
  fp = open(filename, 'r')
  text = fp.read()
  fp.close()
  
  match = re.search(r'\w+_(.+)', filename)
  if match:
    host = 'http://' + match.group(1)
  else:
    sys.exit('Host name not found in filename.')
  
  urls = set()
  match = re.findall(r'GET (\S+/puzzle\S+) HTTP', text)
  if match:
    for relative_url in match:
      urls.add(urlparse.urljoin(host, relative_url))
    return sorted(urls)
  else:
    sys.exit('No URLs found.')


def download_images(img_urls, dest_dir):
  """Given the urls already in the correct order, downloads
  each image into the given directory.
  Gives the images local filenames img0, img1, and so on.
  Creates an index.html in the directory
  with an img tag to show each local image file.
  Creates the directory if necessary.
  """
  # +++your code here+++
  if os.path.exists(dest_dir) == False:
    os.makedirs(dest_dir)
  
  fp = open('index.html', 'w')
  fp.write('<html>\n<body>')
  
  i = 0
  for url in img_urls:
    filename = str.format('img{}.jpg', i)
    filepath = os.path.abspath(os.path.join(dest_dir, filename))
    print 'Downloading image %s' % (filename)
    urllib.urlretrieve(url, filepath)
    fp.write(str.format('<img src="{}" />', filename))
    i += 1

  fp.write('</body>\n</html>')
  fp.close()


def main():
  args = sys.argv[1:]

  if not args:
    print 'usage: [--todir dir] logfile '
    sys.exit(1)

  todir = ''
  if args[0] == '--todir':
    todir = args[1]
    del args[0:2]

  img_urls = read_urls(args[0])

  if todir:
    download_images(img_urls, todir)
  else:
    print '\n'.join(img_urls)

if __name__ == '__main__':
  main()
