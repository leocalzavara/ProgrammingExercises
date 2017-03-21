#!/usr/bin/python
# Copyright 2010 Google Inc.
# Licensed under the Apache License, Version 2.0
# http://www.apache.org/licenses/LICENSE-2.0

# Google's Python Class
# http://code.google.com/edu/languages/google-python-class/

# Leonardo P. Calzavara
# https://github.com/leocalzavara

import sys
import re
import os
import shutil
#import commands # this is unix-only, so i'm replacing for subprocess module
import subprocess

"""Copy Special exercise
"""

# +++your code here+++
# Write functions and modify main() to call them

def is_special(filename):
  # A "special" file is one where the name contains the pattern __w__ somewhere, where the w is one or more word chars.
  match = re.search(r'__\w+__', filename)
  if match:
    return True
  return False

def get_special_paths(dir):
  # returns a list of the absolute paths of the special files in the given directory
  path_list = []
  filenames = os.listdir(dir)
  for filename in filenames:
    if is_special(filename):
      path_list.append(os.path.abspath(os.path.join(dir, filename)))
  return path_list

def copy_to(paths, dir):
  # given a list of paths, copies those files into the given directory
  if os.path.exists(dir) == False:
    os.makedirs(dir)
  for path in paths:
    shutil.copy(path, dir)
  return

def zip_to(paths, zippath):
  # given a list of paths, zip those files up into the given zipfile
  cmd = r'"C:\Program Files\7-Zip\7z.exe" a %s %s' % (zippath, ' '.join(paths))
  print cmd
  #(status, output) = commands.getstatusoutput(cmd)
  output = subprocess.check_output(cmd)
  #if status:
    #sys.stderr.write(output)
    #sys.exit(status)
  return

def main():
  # This basic command line argument parsing code is provided.
  # Add code to call your functions below.

  # Make a list of command line arguments, omitting the [0] element
  # which is the script itself.
  args = sys.argv[1:]
  if not args:
    print "usage: [--todir dir][--tozip zipfile] dir [dir ...]";
    sys.exit(1)

  # todir and tozip are either set from command line
  # or left as the empty string.
  # The args array is left just containing the dirs.
  todir = ''
  if args[0] == '--todir':
    todir = args[1]
    del args[0:2]

  tozip = ''
  if args[0] == '--tozip':
    tozip = args[1]
    del args[0:2]

  if len(args) == 0:
    print "error: must specify one or more dirs"
    sys.exit(1)

  # +++your code here+++
  # Call your functions
  
  special_paths = get_special_paths(args[0])
  
  if todir:
    copy_to(special_paths, todir)
  elif tozip:
    zip_to(special_paths, tozip)
  else:
    print '\n'.join(special_paths)
  
if __name__ == "__main__":
  main()
